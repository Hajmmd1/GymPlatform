# GymPlatform

**Enterprise SaaS platform for gym management, trainer support, member engagement, and business visibility.**

---

## What is GymPlatform

GymPlatform is a modular monolith SaaS solution built with Clean Architecture for managing complete gym business operations. It centralizes member management, workout programming, class scheduling, billing, and real-time communication for gym owners, trainers, and members.

## Key Features

- **Gym Management** — Complete gym setup, multi-location support, member lifecycle management
- **Workout Programming** — Exercise library, program builder, body measurements, progress tracking
- **Class Scheduling** — Room management, session booking, coach availability
- **Financial Management** — Billing, payments, marketplace program listings
- **Real-time Communication** — Chat, notifications, calendar, booking management
- **Admin Dashboard** — Platform-wide reporting, user management, system health

## Current Implementation Status

| Component | Status |
|-----------|--------|
| **Membership Module** | ✅ Complete — Domain + Application + Infrastructure + API + Tests |
| **Training Module** | ✅ Complete — Domain + Application + Infrastructure + API + Migrations |
| **Communication Module** | 🔄 in Progress — Calendar domain complete, Chat/Messaging remaining |
| **Financial Module** | 📋 Scaffolded — awaiting implementation |
| **Frontend** | 📋 Planned — not yet implemented |

---

## Technology Stack

### Backend

| Layer | Technology | Version |
|-------|-----------|---------|
| Framework | ASP.NET Core | 10 |
| Language | C# | 12 |
| ORM | Entity Framework Core | 10.0.9 |
| Database | SQL Server / LocalDB | — |
| Authentication | JWT Bearer with tenant claims | — |
| API Docs | Swagger / OpenAPI (Swashbuckle) | 8.1.0 |
| Testing | xUnit + Moq + FluentAssertions | — |

### Architecture

| Pattern | Technology |
|---------|-----------|
| Architecture | Modular Monolith + Clean Architecture per module |
| Structure | 4 Solution Projects: Api, SharedKernel, Infrastructure, Modules.* |
| Domain | Each module has Domain, Application, Infrastructure layers |
| Testing | Unit tests (>80% coverage target), Integration tests |
| Multi-Tenant | Row-Level Security + EF Core Global Query Filters |

### Frontend (Planned)

| Layer | Technology | Version |
|-------|-----------|---------|
| Framework | Next.js | 15+ |
| UI Library | React | 19+ |
| Language | TypeScript | 5+ |
| Styling | Tailwind CSS | 3+ |
| State | Zustand + TanStack Query | Latest |
| Forms | React Hook Form + Zod | Latest |

---

## Project Structure

```
GymPlatform.sln
├── GymPlatform.Api                        # Composition Root (Minimal APIs, Middleware)
├── GymPlatform.SharedKernel               # BaseEntity, Result<T>, IDomainEvent, IUnitOfWork
├── GymPlatform.Infrastructure             # DbContexts, EF Configurations, Repository Implementations
├── GymPlatform.Modules.Membership/        # ✅ Complete — Gym, Member, Coach aggregates
├── GymPlatform.Modules.Training/          # ✅ Complete — Exercise, WorkoutProgram, etc.
├── GymPlatform.Modules.Financial/         # 📋 Scaffolded — awaiting implementation
├── GymPlatform.Modules.Communication/     # 🔄 In Progress — Session, Booking, Room, Chat
├── GymPlatform.Modules.Membership.Tests/  # ✅ 17 unit tests passing
└── GymPlatform.Modules.Communication.Tests/ # ✅ 3 unit tests passing
```

Each module follows **Clean Architecture**:
```
GymPlatform.Modules.<Module>/
├── Domain/        # Entities, ValueObjects, Events, Exceptions, Repositories (interfaces)
├── Application/   # Commands, Queries, Validators, DTOs, Handlers
└── Infrastructure/ # EF Configurations, Repository Implementations
```

---

## Quick Start

### Prerequisites

- [.NET SDK 10.0.301+](https://dotnet.microsoft.com/download)
- SQL Server or LocalDB
- Git

### Setup

```bash
# Clone repository
git clone https://github.com/your-org/GymPlatform.git
cd GymPlatform

# Restore packages
dotnet restore

# Build
dotnet build

# Run tests
dotnet test

# Run API
cd GymPlatform.Api
dotnet run
```

### Access Points

- **Swagger UI**: `https://localhost:<port>/swagger`
- **Health Check**: `https://localhost:<port>/health`

### Database Migrations

```bash
dotnet ef database update --project GymPlatform.Modules.Membership
dotnet ef database update --project GymPlatform.Modules.Training
dotnet ef database update --project GymPlatform.Modules.Communication
```

---

## Documentation

### Essential Reading

| Document | Purpose |
|----------|---------|
| [docs/DOCUMENTATION_INDEX.md](docs/DOCUMENTATION_INDEX.md) | **Start here** — Complete guide to all documentation |
| [docs/PROJECT_GUIDE_FA.md](docs/PROJECT_GUIDE_FA.md) | Complete project overview in Persian |
| [docs/backend/BACKEND_GUIDE_FA.md](docs/backend/BACKEND_GUIDE_FA.md) | Authoritative backend development guide (EN + FA) |
| [docs/PROJECT_HANDOFF.md](docs/PROJECT_HANDOFF.md) | Current implementation state |
| [docs/IMPLEMENTATION_CHANGES.md](docs/IMPLEMENTATION_CHANGES.md) | Complete change log |

### For AI Agents

| Document | Purpose |
|----------|---------|
| [.ai/agent-rules.md](.ai/agent-rules.md) | **MANDATORY FIRST READ** — Execution rules, cleanup, git policy |
| [.ai/context/WORKSPACE.md](.ai/context/WORKSPACE.md) | Workspace boundaries and context |
| [.ai/context/PROJECT_STATE.md](.ai/context/PROJECT_STATE.md) | Current project status |
| [.ai/context/IMPLEMENTATION_MASTER_PLAN.md](.ai/context/IMPLEMENTATION_MASTER_PLAN.md) | Full implementation roadmap |

### Architecture & Design

| Document | Purpose |
|----------|---------|
| [.ai/context/PRODUCT_BLUEPRINT.md](.ai/context/PRODUCT_BLUEPRINT.md) | Product vision, 21 modules, MVP definition |
| [.ai/context/FUNCTIONAL_REQUIREMENTS.md](.ai/context/FUNCTIONAL_REQUIREMENTS.md) | User stories, business rules, permissions |
| [.ai/context/DATABASE_BLUEPRINT.md](.ai/context/DATABASE_BLUEPRINT.md) | Entity model, relationships, scaling strategy |
| [.ai/context/API_BLUEPRINT.md](.ai/context/API_BLUEPRINT.md) | REST API standards, JWT auth, endpoint catalog |
| [.ai/context/UI_UX_BLUEPRINT.md](.ai/context/UI_UX_BLUEPRINT.md) | Screen specifications, design system |

---

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for detailed contribution guidelines.

**New contributors must read these documents before writing code:**

1. `.ai/agent-rules.md` — Mandatory AI execution rules
2. `docs/PROJECT_GUIDE_FA.md` — Complete project understanding
3. `docs/backend/BACKEND_GUIDE_FA.md` — Architecture and patterns
4. `docs/PROJECT_HANDOFF.md` — Current implementation state
5. `docs/IMPLEMENTATION_CHANGES.md` — Recent changes

---

## Security

See [SECURITY.md](SECURITY.md) for vulnerability reporting policy and security standards.

---

## License

TBD

---

## Module Status

| Module | Domain | Application | Infrastructure | API | Tests | Status |
|--------|--------|-------------|----------------|-----|-------|--------|
| Membership | ✅ | ✅ | ✅ | 4 endpoints | 17 unit tests | Production-ready |
| Training | ✅ | ✅ | ✅ | 7 endpoints | Scaffolded | Ready for integration tests |
| Communication | ✅ (Calendar) | ✅ | ✅ | 6 endpoints | 3 unit tests | Chat/Messaging pending |
| Financial | Scaffolded | — | — | — | — | Awaiting implementation |

### API Endpoints Implemented

**Membership (4 endpoints)**:
- `POST /api/gyms` — Create gym
- `POST /api/members` — Register member
- `POST /api/coaches/{id}/assign` — Assign coach to member
- `POST /api/gyms/{id}/deactivate` — Deactivate gym

**Training (7 endpoints)**:
- `POST /api/exercises` — Create exercise
- `POST /api/workout-programs` — Create workout program
- `POST /api/workout-logs` — Log workout
- `POST /api/exercise-videos` — Upload exercise video
- `POST /api/body-measurements` — Record body measurement
- `POST /api/progress-photos` — Upload progress photo
- `PATCH /api/coach-profiles` — Update coach profile

**Communication Calendar (6 endpoints)**:
- `POST /api/rooms` — Create room
- `POST /api/sessions` — Create session
- `POST /api/bookings` — Book session
- `POST /api/sessions/{id}/cancel` — Cancel session
- `POST /api/bookings/{id}/cancel` — Cancel booking
- `POST /api/coaches/{id}/availability` — Set coach availability
