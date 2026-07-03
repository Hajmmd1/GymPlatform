# Changelog

## Purpose
Records all significant changes, releases, and improvements to GymPlatform.

## Scope
All versions, features, bug fixes, and technical changes.

## Owner
Release Manager / AI Documentation Agent

## Status
Active

## Last Updated
2026-07-03

---

## [Unreleased] — Documentation Sprint

### Added — Documentation

- `docs/DOCUMENTATION_INDEX.md` — Complete bilingual (EN + FA) documentation index with reading order for: Backend Developers, Frontend Developers, AI Agents, Technical Architects, and Project Managers.
- `docs/FRONTEND_DEVELOPER_GUIDE_FA.md` — Persian frontend developer guide covering planned tech stack (Next.js 15, React 19, TypeScript 5), project structure, component architecture, state management, API integration patterns, routing, responsive design, and testing strategy.
- `docs/ai/AI_DEVELOPER_GUIDE_FA.md` — Persian AI agent developer guide covering mandatory execution rules, cleanup policy, git workflow, context layer navigation, implementation patterns, knowledge graph structure, and learning path.
- `docs/frontend/FRONTEND_DEVELOPER_HANDBOOK_FA.md` — Persian frontend handbook with project structure, shadcn/ui setup, TanStack Query hooks, Zustand stores, routing, responsive design, and deployment strategy.
- `.github/PULL_REQUEST_TEMPLATE.md` — PR template with module checklist, mandatory reading verification, and quality gates.
- `.github/ISSUE_TEMPLATE/bug_report.yml` — Bug report issue template with validation checklist.
- `SECURITY.md` — Security policy with vulnerability reporting, security standards, compliance roadmap, and contributor security checklist.

### Changed — Documentation

- `README.md` — Updated to reflect current .NET 10 / SQL Server / EF Core 10.0.9 stack. Added current API endpoint table, Phase status table, and accurate technology table. Removed stale PostgreSQL/Redis/RabbitMQ references.
- `CONTRIBUTING.md` — Added mandatory onboarding section listing all required documents new contributors must read before writing code. Added Clean Architecture overview, conventional commits table, branch naming conventions, and PR checklist with quality gates.
- `docs/backend/BACKEND_GUIDE_FA.md` — Added English section at the top of the Persian guide. Fixed typo in title (lowercase 'b' fixed to 'Back-end'). Enhanced document structure with bilingual table of contents.
- `.ai/agent-rules.md` — Re-stated as MANDATORY FIRST READ across all documentation.

### Changed — Codebase (no implementation change)

- Documentation structure reviewed and consolidated.
- No business logic or application code modified.

---

## [2026-07-02] — Phase 2 Communication Module Calendar Sprint 1

### Added

- Communication Domain Layer — Session, Booking, Room, CoachAvailability entities with domain events
- Communication Application Layer — 6 commands with validators and handlers
- Communication Infrastructure Layer — CommunicationDbContext, EF configurations, Repository implementations
- Communication Unit Tests — 3 handler tests (CreateRoom, CreateSession, BookSession)
- 6 Calendar API endpoints

**Total API endpoints**: 17 (4 Membership + 7 Training + 6 Communication Calendar)

---

## [2026-06-30] — Phase 1 Training Module Sprints 1-3

### Added

- Training Domain Layer — 8 entities, 3 enums, 7 domain events, 7 repository interfaces
- Training Application Layer — 7 commands, 7 validators, 7 handlers, 7 DTO files
- Training Infrastructure Layer — TrainingDbContext, 7 EF configurations, 7 repository implementations
- Training Migration — Initial EF Core migration for Training schema
- 7 Training API endpoints
- Training unit tests scaffold

---

## [2026-06-29] — Phase 0 Foundation Stabilization

### Added

- Membership Value Object format validation (Email RFC 5322, Phone E.164)
- Membership Unit Test Suite — 17 tests covering validators and handlers
- EF Core Migrations — Initial Membership migration
- 4 Membership API endpoints (/api/gyms, /api/members, /api/coaches)
- Swagger/OpenAPI integration
- Health check endpoints (/health, /healthz)

---

## [2026-06-28] — Product Documentation Phases 6-11

### Added

- `.ai/context/PRODUCT_BLUEPRINT.md` — Core product specification (21 modules, user journeys, business model)
- `.ai/context/FUNCTIONAL_REQUIREMENTS.md` — Functional requirements with user stories, business rules, permissions
- `.ai/context/NON_FUNCTIONAL_REQUIREMENTS.md` — Performance, security, scalability requirements
- `.ai/context/DATABASE_BLUEPRINT.md` — Entity design, relationships, scaling strategy
- `.ai/context/API_BLUEPRINT.md` — REST API patterns, versioning, auth flow
- `.ai/context/UI_UX_BLUEPRINT.md` — Screen designs, component library, responsive behavior
- `.ai/context/MASTER_ROADMAP.md` — Phased development, sprints, milestones

---

## [2026-06-17] — v0.1.0 Project Initialization

### Added

- Initial project structure and scaffolding
- Core documentation: MASTER_PRD.md, BUSINESS_RULES.md, USER_ROLES.md, ARCHITECTURE.md, DATABASE.md, API_DESIGN.md, UI_UX.md, ROADMAP.md
- Development rules: project-rules.md, architecture-rules.md, coding-standards.md, backend-rules.md, frontend-rules.md, database-rules.md, security-rules.md, workflow.md
- ADR directory and archived preliminary ADRs
- .gitignore configuration

### Architecture Documentation
- Archived preliminary ADRs moved to `/adr/Archive/`:
  - ADR-001 — PostgreSQL consideration (unapproved)
  - ADR-002 — Clean Architecture consideration (unapproved)
  - ADR-003 — Tech stack consideration (unapproved)

---

## [2026-06-17] — v0.0.0 Initial Project State

Initial project state with no code or documentation.

---

## Version Format

Follows Semantic Versioning: MAJOR.MINOR.PATCH

- **MAJOR**: Breaking changes, major features
- **MINOR**: New features, backward compatible
- **PATCH**: Bug fixes, performance improvements

## How to Read This Changelog

Each version has subsections: Added, Changed, Deprecated, Removed, Fixed, Security
- Features listed under Added
- Breaking changes noted under Changed or Removed
- Security vulnerabilities under Security
