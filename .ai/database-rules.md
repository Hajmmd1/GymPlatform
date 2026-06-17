# Database Rules

## Purpose
Defines database architecture, design principles, and operational standards for GymPlatform data persistence.

## Scope
All database schemas, migrations, queries, and data access patterns.

## Owner
Database Architecture Lead

## Status
Draft

## Last Updated
2026-06-17

---

## Table of Contents
1. [Database Technology](#database-technology)
2. [Design Principles](#design-principles)
3. [Naming Conventions](#naming-conventions)
4. [Schema Design Rules](#schema-design-rules)
5. [Indexing Strategy](#indexing-strategy)
6. [Migration Standards](#migration-standards)
7. [Backup and Recovery](#backup-recovery)
8. [Security Standards](#security-standards)

---

## Database Technology

### Primary Database
- PostgreSQL 16+ (Azure Database for PostgreSQL)
- Unicode support (UTF-8 encoding)
- Read replicas for scaling

### Caching Layer
- Redis 7+ (Azure Cache for Redis)
- Distributed caching strategy
- Session storage

### Search Engine (if needed)
- Elasticsearch or Azure AI Search
- Full-text search capabilities
- Analytics and reporting

---

## Design Principles

### Normalization
- 3NF for transactional data
- Denormalization for read-heavy analytics
- Balance performance vs. consistency

### Soft Delete
- All tables include `is_deleted` boolean
- `deleted_at` timestamp for audit
- Global query filters applied

### Audit Trail
- `created_at`, `created_by` columns
- `updated_at`, `updated_by` columns
- Version tracking for optimistic concurrency

---

## Naming Conventions

### Tables
- Plural snake_case: `users`, `gym_memberships`
- No abbreviations: use `membership` not `mem`
- Prefixes for module separation: `billing_invoices`

### Columns
- snake_case for all columns
- Primary key: `{table_name}_id`
- Foreign key: `{referenced_table}_id`
- Boolean: `is_` or `has_` prefix

### Indexes
- `idx_{table}_{column}`: `idx_users_email`
- `uniq_{table}_{column}`: for unique constraints
- Descriptive names for composite indexes

### Constraints
- `ck_{table}_{condition}`: check constraints
- `fk_{table}_{referenced}`: foreign key constraints

---

## Schema Design Rules

### Primary Keys
- UUIDs for all primary keys
- `gen_random_uuid()` generated in database
- Never expose sequential IDs

### Foreign Keys
- Explicit foreign key constraints
- Cascade delete where appropriate
- No orphaned records policy

### Data Types
- `timestamptz` for all timestamps
- `uuid` for identifiers
- `citext` for case-insensitive text (emails)
- `jsonb` for flexible attributes

### Nullability
- Required fields: `NOT NULL`
- Optional fields: `NULL` allowed
- Default values explicit

---

## Indexing Strategy

### Primary Indexes
- All primary keys indexed automatically
- Foreign key indexes required

### Query Patterns
- Index by common query patterns
- Composite indexes for multi-column queries
- Partial indexes for filtered queries

### Performance Indexes
- Covering indexes for frequent queries
- Expression indexes for computed values
- GIN indexes for jsonb and array searches

### Monitoring
- Index usage statistics tracked
- Unused indexes identified quarterly
- Performance regressions monitored

---

## Migration Standards

### Migration Tools
- EF Core migrations or Flyway
- Migration scripts in version control
- Idempotent migration scripts

### Migration Process
- Migration review required
- Test migrations in staging
- Rollback plans documented

### Naming Convention
- `{version}_{description}`: `001_create_users_table`
- Timestamp prefix optional
- Clear description of change

---

## Backup and Recovery

### Backup Schedule
- Daily full backups
- Hourly incremental backups
- Transaction log backups every 5 minutes

### Retention Policy
- 30 days for daily backups
- 7 days for hourly backups
- Point-in-time recovery enabled

### Recovery Testing
- Monthly restore tests
- RTO/RPO targets defined
- Documentation for restore procedures

---

## Security Standards

### Encryption
- Transparent Data Encryption (TDE) enabled
- Column-level encryption for PII
- Connection encryption (SSL/TLS)

### Access Control
- Database roles for applications
- Read/Write separation
- Admin access restricted

### Auditing
- All DDL changes logged
- Data access for sensitive tables
- Alert on suspicious activity

---

## Sections Prepared for Future Content

### Data Warehouse Architecture
*To be defined*

### Multi-Tenant Database Strategy
*To be defined*

### Disaster Recovery Plan
*To be defined*

### Performance Tuning Guidelines
*To be defined*