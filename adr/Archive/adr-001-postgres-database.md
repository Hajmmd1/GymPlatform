# ADR-001: PostgreSQL for Primary Database

## Status
Draft - Unapproved (Archived for Reference Only)

## Date
2026-06-17

## Note
This ADR was created during project initialization without formal architectural approval. It is archived for reference only and does NOT represent an official architecture decision.

## Context
GymPlatform requires a primary database for storing member data, memberships, classes, bookings, payments, and gym information. The database must support:
- ACID transactions for financial data
- JSON storage for flexible attributes
- Geospatial queries for location services (future)
- Strong consistency for membership status
- Multi-tenant data isolation
- Horizontal scaling capability

Alternatives considered:
- **SQL Server** - Enterprise-grade but higher licensing cost
- **MySQL** - Popular but less advanced features
- **MongoDB** - Flexible but lacks transactional guarantees needed
- **CosmosDB** - Serverless but vendor lock-in

## Decision
Use PostgreSQL 16+ as the primary relational database.

Rationale:
- Open-source with strong community support
- Advanced data types (JSON, arrays, geospatial)
- Excellent ACID compliance
- Row-level security for multi-tenant isolation
- Read replica support for scaling
- Managed service available (Azure Database for PostgreSQL)

## Consequences

### Positive
- No licensing costs
- Advanced features for future needs
- JSONB for flexible schema evolution
- Strong ecosystem of tools
- Excellent performance on analytical queries

### Negative
- Learning curve for team unfamiliar with PostgreSQL
- Connection pooling complexity
- Requires more DBA knowledge than managed alternatives

### Neutral
- Will need to evaluate read replica performance under load
- Migration tools well-established (EF Core migrations)