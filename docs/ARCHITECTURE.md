# System Architecture

## Purpose
Documents the current system architecture, technology stack, and design decisions for GymPlatform. This document reflects the actual implemented and planned architecture.

## Scope
All system components, integration patterns, deployment architecture, and technology choices.

## Owner
Lead Software Architect / AI Documentation Agent

## Status
Active — Updated 2026-07-03

## Last Updated
2026-07-03

---

## Table of Contents
1. [Architecture Overview](#architecture-overview)
2. [Technology Stack](#technology-stack)
3. [System Components](#system-components)
4. [Data Architecture](#data-architecture)
5. [API Architecture](#api-architecture)
6. [Security Architecture](#security-architecture)
7. [Deployment Architecture](#deployment-architecture)
8. [Monitoring and Observability](#monitoring-and-observability)

---

## Architecture Overview

### Architectural Style
**Modular Monolith + Clean Architecture** per module.

GymPlatform uses a single deployable unit (monolith) with clearly bounded modules, each following Clean Architecture layers (Domain → Application → Infrastructure → Composition Root).

This approach provides:
- Single deployment and operational model
- Compile-time boundaries between modules
- Clean Architecture benefits (testability, separation of concerns)
- Future migration path to microservices if needed

### Key Design Principles
- **Clean Architecture** — Domain layer has zero external dependencies
- **CQRS** — Commands for writes, Queries (planned) for reads
- **Domain Events** — In-process event publishing for cross-module coordination
- **Multi-Tenant First** — Every entity has `TenantId`; RLS + Global Query Filter
- **YAGNI + KISS** — Implement only what is needed now
- **Shared Kernel** — Common primitives in `GymPlatform.SharedKernel`

---

## Technology Stack

### Backend (.NET)

| Layer | Technology | Version |
|-------|-----------|---------|
| Framework | ASP.NET Core | **10** |
| Language | C# | **12** |
| ORM | Entity Framework Core | **10.0.9** |
| Database | SQL Server / LocalDB | Latest supported |
| Authentication | JWT Bearer (custom, with tenant claims) | — |
| API Docs | Swashbuckle (Swagger / OpenAPI) | 8.1.0 |
| Testing | xUnit + Moq + FluentAssertions | Latest |
| Migrations | EF Core Migrations | Built-in |

### Solution Structure

```
GymPlatform.sln
├── GymPlatform.Api                      # Composition Root (Minimal APIs, Middleware)
├── GymPlatform.SharedKernel             # BaseEntity, Result<T>, IDomainEvent, IUnitOfWork
├── GymPlatform.Infrastructure           # DbContexts, EF Configurations, Repositories
├── GymPlatform.Modules.Membership/      # Gym, Member, Coach aggregates
├── GymPlatform.Modules.Training/        # Exercise, WorkoutProgram aggregates
├── GymPlatform.Modules.Financial/       # Scaffolded, awaiting implementation
└── GymPlatform.Modules.Communication/   # Session, Booking, Room, Chat/Messaging
```

### Module Mapping

| Solution Project | Modules | Status |
|-----------------|---------|--------|
| `GymPlatform.Modules.Membership` | 1. Membership | ✅ Complete |
| `GymPlatform.Modules.Training` | 2-10. Workout Management through Coach Profiles | ✅ Complete |
| `GymPlatform.Modules.Communication` | 15-21. Chat through Reports & Analytics | 🔄 In Progress |
| `GymPlatform.Modules.Financial` | 11-14. Payments through Reviews & Ratings | 📋 Scaffolded |

### Multi-Tenant Architecture

- **Shared Database, Shared Schema** with `TenantId` discriminator on every table
- **Row-Level Security (RLS)** at database level for defense-in-depth
- **EF Core Global Query Filter** automatically applies tenant filter to all queries
- `TenantId` resolved from JWT `tenant` claim at request start

---

## System Components

### Backend Services

```
GymPlatform.Api                        # Composition Root: Minimal APIs, Middleware
GymPlatform.SharedKernel               # Cross-cutting: BaseEntity, Result, IDomainEvent
GymPlatform.Infrastructure             # Cross-cutting: EF Core, Repositories
GymPlatform.Modules.Membership/        # Module: Domain | Application | Infrastructure
GymPlatform.Modules.Training/          # Module: Domain | Application | Infrastructure
GymPlatform.Modules.Communication/     # Module: Domain | Application | Infrastructure
GymPlatform.Modules.Financial/         # Module: Scaffolded
```

### Shared Kernel Components

| Component | Description |
|-----------|-------------|
| `BaseEntity` | Base for all entities; includes `Id`, `DomainEvents`, `CreatedAt`, `TenantId` |
| `Result<T>` | Functional result type with `IsSuccess`, `Value`, `Error` |
| `IDomainEvent` | Domain event marker interface |
| `DomainEventBase` | Base implementation with `OccurredOn` |
| `IUnitOfWork` | Transaction boundary with `SaveChangesAsync` |
| `ICurrentUserService` | Current user and tenant context |
| `IDateTimeProvider` | DateTime abstraction for testability |
| `Pagination` | `PagedRequest` and `PagedResult<T>` |

### API Layer

- **Minimal APIs** — No controllers; endpoint mapping in `Program.cs`
- **Global Exception Middleware** — RFC 7807 ProblemDetails format
- **CurrentUserService** — Extracts tenant and user from JWT
- **Health Checks** — `/health` and `/healthz` endpoints

---

## Data Architecture

### Database Technology
- **SQL Server** / LocalDB (development)
- **EF Core 10.0.9** for data access
- Separate `DbContext` per module for compile-time boundary enforcement

### Schema Organization

| Schema | Module | DbContext |
|--------|--------|-----------|
| `Membership` | Membership | `MembershipDbContext` |
| `Training` | Training | `TrainingDbContext` |
| `Communication` | Communication | `CommunicationDbContext` |
| `Financial` | Financial | *(pending)* |

### Multi-Tenant Data Strategy

- Every table includes `TenantId` (uniqueidentifier, NOT NULL)
- EF Core `HasQueryFilter()` on every entity
- RLS policies enforced via EF Core migrations
- **No cross-tenant queries possible** when filters are correctly applied

### Migration Strategy

- Each module manages its own migrations folder within `GymPlatform.Infrastructure/Persistence/Migrations/`
- Migrations run via `dotnet ef database update`
- Production migrations via CI/CD with manual approval gate

---

## API Architecture

### Endpoint Organization

```csharp
// Membership (4 endpoints)
POST /api/gyms
POST /api/members
POST /api/coaches/{id}/assign
POST /api/gyms/{id}/deactivate

// Training (7 endpoints)
POST /api/exercises
POST /api/workout-programs
POST /api/workout-logs
POST /api/exercise-videos
POST /api/body-measurements
POST /api/progress-photos
PATCH /api/coach-profiles

// Communication Calendar (6 endpoints)
POST /api/rooms
POST /api/sessions
POST /api/bookings
POST /api/sessions/{id}/cancel
POST /api/bookings/{id}/cancel
POST /api/coaches/{id}/availability
```

### API Standards

| Standard | Implementation |
|----------|---------------|
| Style | REST with RPC elements for domain actions |
| Response | RFC 7807 ProblemDetails for errors |
| Format | JSON |
| Auth | JWT Bearer with `tenant`, `roles`, `permissions` claims |
| Idempotency | Required header for all write operations |
| Versioning | `/v1/` prefix (planned) |

---

## Security Architecture

### Authentication
- Custom JWT Bearer authentication
- JWT structure: `sub` (userId), `tenant` (tenantId), `roles`, `permissions`, `exp`, `iat`, `jti`
- Short-lived access tokens (15 min) + refresh tokens (7 days)
- MFA planned for Platform Admin and Gym Owner roles

### Authorization
- RBAC with role-based policies
- Resource-level permissions per module
- Claims-based: `permissions` array in JWT

### Data Protection
- AES-256 for PII at rest (via SQL Server TDE planned)
- TLS 1.3 in transit
- Tenant isolation via RLS + Global Query Filter

---

## Deployment Architecture

### Environments
- **Development** — Local with LocalDB
- **Staging** — Pre-production testing
- **Production** — Live traffic

### Infrastructure Target (Planned)
- Azure cloud platform
- GitHub Actions CI/CD
- Docker containers
- SQL Server (Azure SQL or self-hosted)

---

## Monitoring and Observability

### Health Checks
- `/health` — Full health check (database connectivity)
- `/healthz` — Liveness probe (simple OK)

### Planned Observability
- OpenTelemetry instrumentation
- Structured logging (Serilog planned)
- Metrics aggregation (planned)

---

## Sections Prepared for Future Content

### Service Mesh Configuration
*Planned for Phase 4+ if microservices migration occurs*

### Database Sharding Strategy
*Planned for Phase 5 when tenant count exceeds shared-db capacity threshold*

### Event Schema Registry
*Planned for Phase 2+ when cross-module event bus is introduced*

### Migration to Microservices
*Long-term option if modular monolith becomes a bottleneck*

---

*End of Architecture Overview — 2026-07-03*
