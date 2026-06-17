# Architecture Decision Records (ADR)

## Purpose
Documents all officially approved architectural decisions for GymPlatform.

## Scope
Technology choices, architectural patterns, framework selections, and design decisions.

## Owner
Lead Software Architect

## Status
Active

## Last Updated
2026-06-17

---

## IMPORTANT - ADR Creation Policy

Architecture Decision Records are only created AFTER a technical decision has been officially approved.

### Process
1. Proposed decision must be documented in a proposal or task
2. Decision must be explicitly approved by stakeholders
3. Only then is an ADR created documenting the approved decision
4. ADRs in `/Archive` are NOT official decisions

### Never Assume
- No ADRs shall be created based on assumptions
- Reference existing ADRs only after official approval
- Documentation in `/docs` is the source of truth

---

## Archived ADRs (Unofficial)

The `/Archive` folder contains early documents created during initialization. These are marked as "Draft - Unapproved" and do NOT represent official architecture decisions.

| Number | Title | Status | Date |
|--------|-------|--------|------|
| 001 | PostgreSQL for Primary Database | Draft - Unapproved | 2026-06-17 |
| 002 | Clean Architecture Pattern | Draft - Unapproved | 2026-06-17 |
| 003 | .NET 8 and Next.js 15 Stack | Draft - Unapproved | 2026-06-17 |

---

## Official ADRs

No official ADRs exist yet. ADRs will be added here after formal approval.

| Number | Title | Status | Date |
|--------|-------|--------|------|
| - | - | - | - |

---

## ADR Format Template

```markdown
# ADR-XXX: {Decision Title}

## Status
Accepted (or Proposed, Deprecated, Superseded)

## Context
{Describe the problem, constraints, and requirements}

## Decision
{What we decided to do and why}

## Consequences
- Positive: {Benefits of this approach}
- Negative: {Drawbacks or trade-offs}
- Neutral: {Other implications}
```

---

## Sources of Truth

| Source | Purpose |
|--------|---------|
| `/docs/*.md` | Official product and architecture documentation |
| `/.ai/*.md` | Official development rules and standards |
| `/.ai/project-rules.md` | Decision protocol and approval requirements |