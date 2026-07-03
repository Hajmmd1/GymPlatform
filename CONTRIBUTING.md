# Contributing to GymPlatform

Thank you for your interest in contributing to GymPlatform! This document outlines the guidelines for contributing to this enterprise SaaS project.

---

## Mandatory Onboarding / ورود اجباری

**⚠️ ALL new contributors MUST read these documents before writing any code:**

**🚨 تمام مشارکت‌کنندگان جدید قبل از نوشتن هرگونه کد، این اسناد را بخوانید:**

| # | Document | Purpose | Time |
|---|----------|---------|------|
| 1 | [`.ai/agent-rules.md`](.ai/agent-rules.md) | **MANDATORY** AI execution rules, cleanup policy, git policy | 10 min |
| 2 | [`docs/DOCUMENTATION_INDEX.md`](docs/DOCUMENTATION_INDEX.md) | Guide to all documentation files and reading order | 10 min |
| 3 | [`docs/PROJECT_GUIDE_FA.md`](docs/PROJECT_GUIDE_FA.md) | Complete project understanding (architecture, modules, workflow) | 45 min |
| 4 | [`docs/backend/BACKEND_GUIDE_FA.md`](docs/backend/BACKEND_GUIDE_FA.md) | Backend architecture, Clean Architecture, CQRS, patterns | 60 min |
| 5 | [`docs/PROJECT_HANDOFF.md`](docs/PROJECT_HANDOFF.md) | Current implementation state and remaining tasks | 20 min |
| 6 | [`docs/IMPLEMENTATION_CHANGES.md`](docs/IMPLEMENTATION_CHANGES.md) | Recent implementation changes and patterns | 15 min |

**For Frontend contributors** (when frontend implementation begins):

| # | Document | Purpose |
|---|----------|---------|
| 7 | [`docs/frontend/FRONTEND_DEVELOPER_HANDBOOK_FA.md`](docs/frontend/FRONTEND_DEVELOPER_HANDBOOK_FA.md) | Frontend architecture and patterns |

**For Technical Architects and AI Agents**:

| # | Document | Purpose |
|---|----------|---------|
| 7 | [`.ai/context/IMPLEMENTATION_MASTER_PLAN.md`](.ai/context/IMPLEMENTATION_MASTER_PLAN.md) | Full implementation roadmap with architectural decisions |
| 8 | [`.ai/context/PRODUCT_BLUEPRINT.md`](.ai/context/PRODUCT_BLUEPRINT.md) | Product vision, 21 modules, MVP definition |

> **Note**: After reading onboarding documents, complete the setup below and then review at least one fully implemented module (Membership is recommended) to understand code patterns before contributing.

---

## Development Setup

### Prerequisites

- [.NET SDK 10.0.301+](https://dotnet.microsoft.com/download)
- SQL Server or LocalDB
- Git
- Visual Studio 2022 or Rider (recommended)
- Node.js 20+ (when frontend begins)

### Clone and Setup

```bash
# Clone repository
git clone https://github.com/your-org/GymPlatform.git
cd GymPlatform

# Restore and build
dotnet restore
dotnet build

# Run tests
dotnet test

# Run API locally
cd GymPlatform.Api
dotnet run
```

### Running with Hot Reload

```bash
cd GymPlatform.Api
dotnet watch run
```

### Required Environment Configuration

Configure `GymPlatform.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Server=(localdb)\\mssqllocaldb;Database=GymPlatform;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "Jwt": {
    "Key": "dev-secret-key-change-in-production",
    "Issuer": "GymPlatform",
    "Audience": "GymPlatformClients",
    "ExpiryMinutes": 60
  }
}
```

### Database Migrations

```bash
dotnet ef migrations add <MigrationName> --project GymPlatform.Modules.Membership
dotnet ef database update --project GymPlatform.Modules.Membership
```

Install EF Core CLI if needed:
```bash
dotnet tool install --global dotnet-ef
```

---

## Architecture Overview

GymPlatform uses **Modular Monolith + Clean Architecture**:

```
GymPlatform.sln
├── GymPlatform.Api                    (Composition Root)
├── GymPlatform.SharedKernel           (BaseEntity, Result<T>, IDomainEvent, IUnitOfWork)
├── GymPlatform.Infrastructure         (EF Core DbContexts, Repositories, DI)
├── GymPlatform.Modules.Membership/      (Gym, Member, Coach)
├── GymPlatform.Modules.Training/        (Exercise, WorkoutProgram, etc.)
├── GymPlatform.Modules.Financial/       (Planned)
└── GymPlatform.Modules.Communication/   (Session, Booking, Room, Chat)
```

### Clean Architecture Layers

| Layer | Responsibility | Dependencies |
|-------|---------------|--------------|
| **Domain** | Entities, ValueObjects, Domain Events, Repository interfaces | SharedKernel only |
| **Application** | Commands, Queries, Validators, DTOs, Handlers | Domain only |
| **Infrastructure** | EF Core configurations, Repository implementations | Domain + Application |
| **Api** | Minimal API endpoints, Middleware, DI registration | All above |

---

## Branching Strategy

```
main (production-ready code)
  ↑
develop (integration branch)
  ↑
feature/<module>-<description> (new features)
hotfix/<description> (urgent fixes)
```

### Branch Naming Convention

- `feature/membership-add-member-status` — New feature
- `fix/training-command-validator` — Bug fix
- `docs/update-readme` — Documentation
- `refactor/membership-repository` — Code refactoring
- `test/add-integration-tests` — Test additions

---

## Conventional Commits

All commits must follow [Conventional Commits](https://www.conventionalcommits.org/):

| Type | Description | Example |
|------|-------------|---------|
| `feat` | New feature | `feat: add coach profile update endpoint` |
| `fix` | Bug fix | `fix: resolve validation error in RegisterMember` |
| `docs` | Documentation | `docs: update GETTING_STARTED guide` |
| `refactor` | Code refactoring | `refactor: simplify handler orchestration` |
| `test` | Test additions | `test: add BookingCommandHandler tests` |
| `chore` | Maintenance | `chore: update NuGet packages` |
| `perf` | Performance | `perf: optimize query index on Member` |
| `style` | Code style | `style: format with dotnet format` |

### Commit Message Format

```
<type>(<scope>): <short description>

[optional body]

[optional footer]
```

**Examples:**

```
feat(training): add body measurement command handler

Implements RecordBodyMeasurementCommand with validator and handler.
Follows CQRS pattern with Result<T> return type.

Closes #42
```

---

## Pull Request Process

### 1. Before Starting

- Ensure you have read all documents in the [Mandatory Onboarding](#mandatory-onboarding--ورود-اجباری) section.
- Pick up a task from the current sprint.
- Review related code in existing modules to understand patterns.

### 2. Development Checklist

- [ ] Follow Clean Architecture boundaries strictly.
- [ ] Create Entity/ValueObject in Domain layer (no external dependencies).
- [ ] Create Command/Validator/Handler in Application layer.
- [ ] Create EF Configuration and Repository in Infrastructure.
- [ ] Register new services in `InfrastructureServiceCollectionExtensions.cs`.
- [ ] Register API endpoints in `GymPlatform.Api/Program.cs`.
- [ ] Add unit tests with >80% coverage target.
- [ ] Run `dotnet build` — must pass with zero errors.
- [ ] Run `dotnet test` — all tests must pass.
- [ ] Verify multi-tenant isolation (TenantId filter applied).
- [ ] Verify clean separation — no business logic in Validators or Repositories.

### 3. Before Submitting PR

- [ ] Self-review your code.
- [ ] Ensure `dotnet format` passes.
- [ ] Update `docs/IMPLEMENTATION_CHANGES.md` with new files.
- [ ] Update `docs/PROJECT_HANDOFF.md` if applicable.
- [ ] Write a clear PR description.

### 4. PR Template

When submitting a PR, fill out the `.github/PULL_REQUEST_TEMPLATE.md`.

### 5. Review Process

- At least 1 approval required before merge.
- All CI checks must pass.
- Architect approval required for architectural changes.
- PR must be rebased on latest `develop` before merge.

---

## Code Standards

### General Rules

- Follow the patterns established in `docs/backend/BACKEND_GUIDE_FA.md`.
- All domain logic must be in the Domain layer (not in validators or repositories).
- All entities must inherit from `BaseEntity`.
- Use `internal sealed` for Commands, Handlers, and Validators.
- Use `Result<T>` pattern for all business operations — no exceptions for expected failures.
- All async methods must accept `CancellationToken`.
- Use constructor injection only — no property injection.
- All entities must have a private parameterless constructor for EF Core.

### Naming Conventions

| Element | Convention | Example |
|---------|-----------|---------|
| Module | PascalCase | `GymPlatform.Modules.Training` |
| Entity | PascalCase (singular) | `Exercise`, `WorkoutProgram` |
| Repository Interface | `I` + Entity + Repository | `IExerciseRepository` |
| Command | Action + Entity + `Command` | `CreateExerciseCommand` |
| Handler | Action + Entity + `Handler` | `CreateExerciseCommandHandler` |
| Validator | Action + Entity + `Validator` | `CreateExerciseCommandValidator` |
| DTO | Entity + Request/Response | `CreateExerciseRequest` |
| Domain Event | Past tense | `ExerciseCreated` |
| Value Object | Descriptive | `Email`, `Phone`, `Equipment` |

### Forbidden Patterns

- ❌ Business logic in Validators
- ❌ Business logic in Repositories
- ❌ Domain layer depending on Infrastructure
- ❌ Hard-coded TenantId
- ❌ Using `async void` methods
- ❌ Using `any` type (use proper types)
- ❌ Throwing exceptions for expected business failures

---

## Testing

### Coverage Requirements

| Layer | Tool | Target |
|-------|------|--------|
| Unit | xUnit + Moq + FluentAssertions | >80% of Application layer |
| Integration | TestContainers (SQL Server) | All repository workflows |

### Running Tests

```bash
# All tests
dotnet test

# Specific project
dotnet test GymPlatform.Modules.Membership.Tests

# With coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Test Structure

```
GymPlatform.Modules.<Module>.Tests/
├── Application/
│   └── Commands/
│       └── <Action>/
│           └── <Action>CommandHandlerTests.cs
└── Domain/
    └── ValueObjects/
        └── <VO>Tests.cs
```

### Example Test Pattern

```csharp
[Fact]
public async Task HandleAsync_WithValidCommand_ShouldCreateEntity()
{
    // Arrange
    var mockRepo = new Mock<IRepository>();
    var mockUow = new Mock<IUnitOfWork>();
    var mockValidator = new Mock<IValidator<Command>>();
    var mockCurrentUser = new Mock<ICurrentUserService>();

    mockValidator.Setup(v => v.Validate(It.IsAny<Command>()))
        .Returns(Result.Success());
    mockCurrentUser.Setup(c => c.TenantId).Returns(Guid.NewGuid());

    var handler = new CommandHandler(mockRepo.Object, mockUow.Object, ...);

    // Act
    var result = await handler.HandleAsync(command, CancellationToken.None);

    // Assert
    result.IsSuccess.Should().BeTrue();
    mockRepo.Verify(r => r.AddAsync(It.IsAny<Entity>(), It.IsAny<CancellationToken>()), Times.Once);
}
```

---

## Module Development Pattern

When adding a new module or command:

1. **Domain Layer**: Create Entity/ValueObject with business rules and domain events.
2. **Application Layer**: Create Command record, Validator, Handler, and DTOs.
3. **Infrastructure Layer**: Create EF Configuration and Repository implementation.
4. **Api Layer**: Register services in DI and add Minimal API endpoint.
5. **Tests**: Add unit tests for Validator and Handler.
6. **Documentation**: Update `IMPLEMENTATION_CHANGES.md` and `PROJECT_HANDOFF.md`.

---

## Issue Reporting

### Bug Reports

Please include:
- Steps to reproduce
- Expected vs actual behavior
- .NET SDK version
- Environment (LocalDB, SQL Server)

### Feature Requests

Please include:
- Business justification
- Module it belongs to
- Acceptance criteria
- Estimated effort

---

## Questions?

- Check the documentation index at [`docs/DOCUMENTATION_INDEX.md`](docs/DOCUMENTATION_INDEX.md)
- Review the [Backend Developer Guide](docs/backend/BACKEND_GUIDE_FA.md)
- Contact the Lead Software Architect or open a GitHub Discussion
