# Database Design

## Purpose
Documents the actual database architecture for GymPlatform including SQL Server schema, EF Core entity configurations, multi-tenant strategy, and migration approach.

## Scope
SQL Server schema design, EF Core DbContext configurations, entity relationships, multi-tenant isolation, indexing strategy, and migration workflow.

## Owner
Database Architecture Lead / AI Documentation Agent

## Status
Active — Updated 2026-07-03

## Last Updated
2026-07-03

> **Note**: This document reflects current implementation. For conceptual database design, refer to `.ai/context/DATABASE_BLUEPRINT.md`. For actual entity models, see the Infrastructure project EF Configurations.

---

## Table of Contents
1. [Database Technology](#database-technology)
2. [Module Schema Organization](#module-schema-organization)
3. [Multi-Tenant Strategy](#multi-tenant-strategy)
4. [Entity Model Mapping](#entity-model-mapping)
5. [Indexing Strategy](#indexing-strategy)
6. [Migration Strategy](#migration-strategy)
7. [Performance Optimization](#performance-optimization)

---

## Database Technology

| Technology | Version | Notes |
|-----------|---------|-------|
| Database Engine | SQL Server / LocalDB | Latest supported |
| ORM | Entity Framework Core | 10.0.9 |
| Migrations | EF Core Migrations | Built-in tooling |
| Design-time Factory | `IDesignTimeDbContextFactory<T>` | Required for migrations |

### Current DbContexts

| DbContext | Module | Schema | Status |
|-----------|--------|--------|--------|
| `MembershipDbContext` | Membership | `Membership` | ✅ Implemented |
| `TrainingDbContext` | Training | `Training` | ✅ Implemented |
| `CommunicationDbContext` | Communication | `Communication` | ✅ Implemented |
| `FinancialDbContext` | Financial | `Financial` | 📋 Planned |

---

## Module Schema Organization

### Membership Schema (`Membership`)

| Table | Entity | Key Fields |
|-------|--------|-----------|
| `Gyms` | `Gym` | Id, Name, Phone, TenantId, IsActive |
| `Members` | `Member` | Id, GymId, FullName, Email, Phone, TenantId |
| `Coaches` | `Coach` | Id, GymId, FullName, Email, TenantId |

### Training Schema (`Training`)

| Table | Entity | Key Fields |
|-------|--------|-----------|
| `Exercises` | `Exercise` | Id, Name, Description, Category, Difficulty, TenantId |
| `WorkoutPrograms` | `WorkoutProgram` | Id, Name, Description, IsPublished, TenantId |
| `ProgramExercises` | `ProgramExercise` | Id, WorkoutProgramId, ExerciseId, Sets, Reps, DisplayOrder |
| `WorkoutLogs` | `WorkoutLog` | Id, MemberId, WorkoutProgramId, Date, TenantId |
| `ExerciseVideos` | `ExerciseVideo` | Id, ExerciseId, Url, IsApproved, TenantId |
| `BodyMeasurements` | `BodyMeasurement` | Id, MemberId, Type, Value, Unit, RecordedAt, TenantId |
| `ProgressPhotos` | `ProgressPhoto` | Id, MemberId, Url, TakenAt, Notes, TenantId |
| `CoachProfiles` | `CoachProfile` | Id, CoachId, Specialties, TenantId |
| `Certifications` | `Certification` | (owned by CoachProfile) |

### Communication Schema (`Communication`)

| Table | Entity | Key Fields |
|-------|--------|-----------|
| `Rooms` | `Room` | Id, Name, Capacity, IsActive, TenantId |
| `Sessions` | `Session` | Id, RoomId, CoachId, Type, StartTime, EndTime, TenantId |
| `Bookings` | `Booking` | Id, SessionId, MemberId, Status, TenantId |
| `CoachAvailability` | `CoachAvailability` | Id, CoachId, StartTime, EndTime, TenantId |

### All Tables Require
- `Id` (uniqueidentifier, primary key, default NEWSEQUENTIALID())
- `TenantId` (uniqueidentifier, NOT NULL, indexed)
- `CreatedAt` (datetimeoffset, NOT NULL)
- `IsDeleted` (bit, NOT NULL, DEFAULT 0) — soft delete planned

---

## Multi-Tenant Strategy

### Strategy: Shared Database, Shared Schema

All tenants share the same database and tables. Data isolation is enforced via:

1. **Database Level (Defense in Depth)**: Row-Level Security (RLS) policies
2. **Application Level (Primary)**: EF Core Global Query Filters

### EF Core Global Query Filter

```csharp
// Example in MembershipDbContext
modelBuilder.Entity<Gym>()
    .HasQueryFilter(g => g.TenantId == _currentTenantService.TenantId);
modelBuilder.Entity<Member>()
    .HasQueryFilter(m => m.TenantId == _currentTenantService.TenantId);
modelBuilder.Entity<Coach>()
    .HasQueryFilter(m => m.TenantId == _currentTenantService.TenantId);
```

### TenantId Resolution

```csharp
// ICurrentUserService extracts from JWT
public interface ICurrentUserService
{
    Guid? TenantId { get; }  // From JWT "tenant" claim
    Guid? UserId { get; }    // From JWT "sub" claim
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
}
```

### Mandatory Rules

- ✅ Every entity MUST have `TenantId` (NOT NULL)
- ✅ Every entity MUST have `HasQueryFilter()` in DbContext
- ✅ `TenantId` MUST be set from `ICurrentUserService`, never hardcoded
- ❌ Never write raw SQL without a `TenantId` filter
- ❌ Never disable global query filters (`IgnoreQueryFilters()` should never be used)

---

## Entity Model Mapping

### Membership Module (Implemented)

```
Gym (Aggregate Root)
├── Id, Name, Phone, TenantId, IsActive, CreatedAt
└── Navigation: Members, Coaches

Member (Aggregate Root)
├── Id, GymId, FullName, Email (VO), Phone (VO), TenantId, Status, CreatedAt
└── Navigation: Gym

Coach (Aggregate Root)
├── Id, GymId, FullName, Email (VO), Phone (VO), TenantId, CreatedAt
└── Navigation: Gym
```

### Training Module (Implemented)

```
Exercise (Aggregate Root)
├── Id, Name, Description, Category, Difficulty, MuscleGroups (VO), Equipment (VO), TenantId

WorkoutProgram (Aggregate Root)
├── Id, Name, Description, IsPublished, Tags, Version, TenantId
└── Contains: ProgramExercures, ExerciseVideos

WorkoutLog (Aggregate Root)
├── Id, MemberId, WorkoutProgramId, CompletedAt, Notes, TenantId

ExerciseVideo (Owned by Exercise)
├── Id, ExerciseId, VideoUrl, IsApproved, TenantId

BodyMeasurement (Aggregate Root)
├── Id, MemberId, Type, Value, Unit, RecordedAt, TenantId

ProgressPhoto (Aggregate Root)
├── Id, MemberId, Url, TakenAt, Notes, IsPrivate, TenantId

CoachProfile (Aggregate Root)
├── Id, CoachId, Specialties, Certifications (Owned), TenantId
```

### Communication Module (Implemented)

```
Room (Aggregate Root)
├── Id, Name, Capacity, IsActive, TenantId, CreatedAt

Session (Aggregate Root)
├── Id, RoomId, CoachId, Type, StartTime, EndTime, TenantId, CreatedAt
└── Contains: Bookings

Booking (Entity, child of Session)
├── Id, SessionId, MemberId, Status, TenantId, CreatedAt

CoachAvailability (Aggregate Root)
├── Id, CoachId, StartTime, EndTime, TenantId, CreatedAt
```

---

## Indexing Strategy

### Recommended Indexes

| Table | Index | Type | Purpose |
|-------|-------|------|---------|
| All entities with `TenantId` | `IX_{Table}_TenantId` | Non-clustered | Tenant filtering (automatically used by EF filters) |
| `Members` | `IX_Members_GymId_TenantId` | Composite | Lookup members by gym |
| `Members` | `IX_Members_Email_TenantId` | Unique | Email uniqueness per tenant |
| `Sessions` | `IX_Sessions_CoachId_StartTime` | Composite | Coach schedule lookups |
| `Sessions` | `IX_Sessions_RoomId_StartTime` | Composite | Room availability checks |
| `Bookings` | `IX_Bookings_MemberId` | Non-clustered | Member booking history |
| `Bookings` | `IX_Bookings_SessionId` | Non-clustered | Session booking list |
| `CoachAvailability` | `IX_CoachAvailability_CoachId` | Non-clustered | Coach schedule queries |
| `WorkoutLogs` | `IX_WorkoutLogs_MemberId` | Non-clustered | Member progress |

### Index Naming Convention

```
IX_{TableName}_{ColumnName}         # Single column
IX_{TableName}_{Col1}_{Col2}        # Composite
UQ_{TableName}_{Column}             # Unique constraint
```

---

## Migration Strategy

### Module-Level Migrations

Each module has its own DbContext and manages its own migrations:

```bash
# Generate migration for Membership
dotnet ef migrations add AddMemberPhone --project GymPlatform.Modules.Membership

# Generate migration for Training
dotnet ef migrations add AddProgressPhoto --project GymPlatform.Modules.Training

# Generate migration for Communication
dotnet ef migrations add AddBookingStatus --project GymPlatform.Modules.Communication
```

### Migration Conventions

- All migrations stored in: `GymPlatform.Infrastructure/Persistence/Migrations/`
- Naming: `YYYYMMDDHHMMSS_DescriptiveName`
- Migrations must be **additive only** unless explicitly authorized
- Multi-tenant RLS policies added via migrations
- Always include `Up()` and `Down()` methods

### Production Migration Process

1. Create and review migration in feature branch
2. Test locally with `dotnet ef database update`
3. Run integration tests against migrated database
4. Merge PR to `develop`
5. Manual approval gate in CI/CD pipeline
6. Execute migration on staging, then production

---

## Performance Optimization

### Query Optimization

| Technique | When Used | Implementation |
|-----------|-----------|----------------|
| AsNoTracking | Read-only queries | `.AsNoTracking()` in repository queries |
| Split Queries | Large includes | `.AsSplitQuery()` for nested collections |
| Pagination | List endpoints | `Skip().Take()` with page and page size |
| Index optimization | Frequent filters | Add composite indexes for common filter combos |
| Query Filters | Tenant filtering | Automatic via `HasQueryFilter()` |

### EF Core Configuration Patterns

```csharp
// Example configuration pattern
public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members", "Membership");
        builder.HasKey(m => m.Id);
        
        // Property configurations
        builder.Property(m => m.FullName).HasMaxLength(200).IsRequired();
        builder.Property(m => m.Email).HasMaxLength(320).IsRequired();
        
        // Relationships
        builder.HasOne(m => m.Gym)
            .WithMany(g => g.Members)
            .HasForeignKey(m => m.GymId);
        
        // Indexes
        builder.HasIndex(m => new { m.GymId, m.TenantId });
        builder.HasIndex(m => new { m.Email, m.TenantId }).IsUnique();
        
        // Global query filter
        builder.HasQueryFilter(m => m.TenantId == GetTenantId());
    }
}
```

---

## Backup and Recovery

### Backup Schedule (Planned)
- Daily full backups via SQL Server Agent
- Hourly transaction log backups
- 30-day retention period

### Recovery Objectives
- **RTO**: 4 hours
- **RPO**: 5 minutes (transaction log backups)

---

## Design Patterns Used

| Pattern | Usage |
|---------|-------|
| Repository Pattern | Data access abstraction per module |
| Unit of Work | Transaction boundary (`IUnitOfWork`) |
| CQRS | Commands for writes, minimal reads |
| Domain Events | In-process cross-module communication |
| Specification Pattern | (Planned) Complex query logic |
| Value Object | Immutable concepts (Email, Phone, Equipment) |
| Aggregate Root | Entity boundary enforcement |

---

*End of Database Design — 2026-07-03*
