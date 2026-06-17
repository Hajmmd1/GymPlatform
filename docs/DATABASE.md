# Database Design

## Purpose
Documents database schema, entity relationships, indexing strategy, and data access patterns.

## Scope
PostgreSQL schema, migration strategy, performance optimization, and data integrity rules.

## Owner
Database Architecture Lead

## Status
Draft - Under Review

## Last Updated
2026-06-17

> **Note:** This document contains preliminary database design considerations. All technology choices are subject to formal approval.

---

## Table of Contents
1. [Database Overview](#database-overview)
2. [Entity Relationship Diagram](#entity-relationship-diagram)
3. [Core Schema](#core-schema)
4. [Indexing Strategy](#indexing-strategy)
5. [Migration Strategy](#migration-strategy)
6. [Performance Optimization](#performance-optimization)
7. [Backup and Recovery](#backup-and-recovery)

---

## Database Overview

### Database Technology
- PostgreSQL 16+ (Azure Database for PostgreSQL Flexible Server)
- Read replicas for scaling reads
- Logical replication for multi-region

### Schema Organization
- `public` schema for core entities
- `audit` schema for audit trails
- `analytics` schema for reporting (if separate)

### Naming Convention
- Tables: plural snake_case (`users`, `gym_memberships`)
- Columns: snake_case (`first_name`, `created_at`)
- Indexes: `idx_{table}_{columns}` (`idx_users_email`)
- Constraints: `ck_{table}_{condition}`, `fk_{table}_{ref}`

---

## Entity Relationship Diagram

### Core Entities
```
Organizations (1) ←→ (M) Locations
     ↑                        ↑
     │                        │
     ↓                        ↓
  Memberships (M) ←→ (M) Members
     ↑                        ↑
     │                        │
     ↓                        ↓
  Payments           Classes ←→ (M) Bookings
                             ↑
                             │
                    Trainers ←→ (M) TrainerSchedules
```

### Entity Descriptions
- **Organizations** - Gym business entities
- **Locations** - Physical gym locations
- **Members** - Gym members/customers
- **Memberships** - Membership plans and status
- **Classes** - Scheduled fitness classes
- **Trainers** - Fitness instructors
- **Bookings** - Class reservations
- **Payments** - Billing transactions

---

## Core Schema

### Organizations Table
```sql
CREATE TABLE organizations (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(255) NOT NULL,
    subdomain VARCHAR(100) UNIQUE NOT NULL,
    email VARCHAR(255) NOT NULL,
    phone VARCHAR(50),
    address TEXT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL,
    is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
    deleted_at TIMESTAMPTZ
);
```

### Members Table
```sql
CREATE TABLE members (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    organization_id UUID NOT NULL REFERENCES organizations(id),
    location_id UUID NOT NULL REFERENCES locations(id),
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    email CITEXT UNIQUE NOT NULL,
    phone CITEXT,
    date_of_birth DATE,
    emergency_contact JSONB,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL,
    is_deleted BOOLEAN NOT NULL DEFAULT FALSE,
    deleted_at TIMESTAMPTZ
);
```

### Classes Table
```sql
CREATE TABLE classes (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    location_id UUID NOT NULL REFERENCES locations(id),
    trainer_id UUID REFERENCES trainers(id),
    name VARCHAR(255) NOT NULL,
    description TEXT,
    start_time TIMESTAMPTZ NOT NULL,
    end_time TIMESTAMPTZ NOT NULL,
    capacity INTEGER NOT NULL CHECK (capacity > 0),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL,
    is_deleted BOOLEAN NOT NULL DEFAULT FALSE
);
```

---

## Indexing Strategy

### Primary Indexes
- All primary keys: automatic B-tree index
- All foreign keys: explicit index
- Email fields: unique index

### Query Pattern Indexes
- `idx_members_email_unique` - Member login
- `idx_classes_start_time` - Schedule queries
- `idx_bookings_member_id` - Member bookings
- `idx_payments_status_date` - Payment reporting

### Performance Indexes
- `idx_members_checkin` - Composite index (location_id, created_at)
- `idx_classes_trainer_time` - Composite index (trainer_id, start_time)
- Partial index for active records only

---

## Migration Strategy

### Migration Tool
- EF Core Migrations OR Flyway
- Migration scripts in `/infra/database/migrations/`

### Migration Process
1. Create migration script
2. Review with team
3. Test in staging environment
4. Deploy with deployment

### Migration Conventions
- Numbered sequentially
- Up/down scripts required
- Idempotent where possible

---

## Performance Optimization

### Query Optimization
- EXPLAIN ANALYZE for complex queries
- Connection pooling (PgBouncer)
- Read replicas for reporting

### Partitioning
- Time-based partitioning for logs
- Tenant-based partitioning for analytics
- Manual partition management

### Materialized Views
- Daily summary tables
- Cached in Redis
- Refresh via scheduled jobs

---

## Backup and Recovery

### Backup Schedule
- Daily full backups at 2 AM UTC
- Hourly incremental backups
- Transaction log backups every 5 minutes

### Recovery Objectives
- RTO: 4 hours
- RPO: 5 minutes

### Backup Storage
- Geo-redundant storage
- 30-day retention
- Point-in-time recovery enabled

---

## Sections Prepared for Future Content

### Analytics Schema
*To be defined*

### Audit Trail Design
*To be defined*

### Multi-Tenant Data Isolation
*To be defined*

### Performance Benchmarks
*To be defined*