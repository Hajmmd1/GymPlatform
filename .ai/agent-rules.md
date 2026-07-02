# AI Execution Rules

**Effective Date**: 2026-07-02  
**Version**: 1.0  
**Audience**: All AI agents working on GymPlatform

## Core Execution Principles

- **Always read project context before starting.** Review `.ai/context/WORKSPACE.md`, `.ai/context/PROJECT_STATE.md`, and `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` before any implementation work.

- **Never guess requirements.** All technical decisions must align with documented blueprints in `.ai/context/` and `docs/`. If a decision is unclear, seek clarification before proceeding.

- **Never overwrite completed work.** Existing implementation that meets documented requirements should not be replaced without explicit requirement changes.

- **Always continue from the first unfinished task.** When resuming work, identify the earliest incomplete work item in the current sprint and proceed sequentially.

- **Never recreate existing files unless required.** Search the codebase thoroughly before creating new files to avoid duplication.

- **Always update project documentation after implementation.** Every completed task must be reflected in `docs/IMPLEMENTATION_CHANGES.md` and `docs/PROJECT_HANDOFF.md`.

- **Always keep IMPLEMENTATION_CHANGES.md and PROJECT_HANDOFF.md synchronized.** Documentation changes must be mirrored across both files to maintain consistency.

- **Always verify the build before finishing.** Run `dotnet build` and ensure zero warnings as errors before ending any session.

- **Always run tests when applicable.** Execute `dotnet test` to validate implementation changes. Minimum 80% coverage required for application layer code.

- **Always use MCP Playwright for UI/API validation when available.** End-to-end testing must use the available Playwright MCP for browser-based validation.

- **Never finish a sprint with failing compilation.** The codebase must compile cleanly before any work is considered complete.

- **Never stop in the middle of a sprint unless a real blocker exists.** Document blockers clearly and escalate immediately.

---

# Mandatory Resource Cleanup Policy (Permanent)

This policy MUST be followed after EVERY heavy task, sprint, implementation, testing session, or validation session.

No background process may remain running after completion.

If any runtime or external process was started, it MUST be properly stopped and disposed.

Examples include but are not limited to:

- JS Runtime
- Node.js
- npm
- pnpm
- yarn
- Vite
- Playwright
- Chromium
- Chrome
- Browser Contexts
- Browser Pages
- dotnet watch
- ASP.NET Development Server
- Kestrel
- TestHost
- MSTest
- xUnit
- Docker containers
- Local databases
- File watchers
- Background workers
- Timers
- IDisposable objects
- IAsyncDisposable objects
- Temporary files
- Open streams
- HttpClient instances created manually
- DbContexts created manually

Before ending EVERY session the agent MUST:

- Dispose Playwright
- Close Browser Pages
- Dispose Browser Contexts
- Stop Chromium
- Stop JS Runtime
- Stop Node processes created by the task
- Stop Vite
- Stop npm/pnpm/yarn processes
- Stop dotnet watch
- Stop ASP.NET Development Server
- Stop TestHost
- Stop background workers
- Dispose DI scopes
- Dispose DbContexts
- Dispose manually created HttpClients
- Release file handles
- Delete temporary resources
- Release memory-intensive resources

The agent must verify that no unnecessary process created during the task remains running.

---

# Mandatory Git Policy

Whenever ALL of the following are successful:

- Build
- Tests
- Validation
- Documentation

The agent MUST automatically execute:

```bash
git status
git add .
git commit -m "<Conventional Commit>"
git push origin <current-branch>
```

If no remote exists, report it.

If push fails, retry once.

---

# Mandatory End-of-Task Checklist

Every implementation session must end with a report containing:

- Phase
- Sprint
- Completed Tasks
- Remaining Tasks
- Build Status
- Test Status
- Validation Status
- Documentation Status
- Cleanup Status
- Git Status
- Commit Hash
- Push Status

---

# Task Constraints

## Prohibited Actions

These actions are strictly prohibited during execution:

- **DO NOT modify application code** unless explicitly implementing a feature from the master plan.
- **DO NOT modify business logic** without documented requirements.
- **DO NOT modify architecture** beyond documented patterns.
- **DO NOT create APIs** outside the module structure defined in `IMPLEMENTATION_MASTER_PLAN.md`.
- **DO NOT create database objects** without documented entity specifications.
- **DO NOT create migrations** without updated entity models.

## Required Reading Before Implementation

Before starting ANY implementation work, AI agents MUST read:

1. `.ai/context/WORKSPACE.md` - Workspace context and purpose
2. `.ai/context/PROJECT_STATE.md` - Current project state
3. `.ai/agent-rules.md` - This file (mandatory rules)
4. `.ai/context/IMPLEMENTATION_MASTER_PLAN.md` - Implementation roadmap
5. Relevant module documentation in `.ai/context/` or `docs/`