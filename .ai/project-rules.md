# Project Rules

## Purpose
Defines the core principles, policies, and operational guidelines for the GymPlatform enterprise SaaS platform.

## Scope
All team members, contributors, and AI assistants working on the GymPlatform codebase.

## Owner
Lead Software Architect

## Status
Draft

## Last Updated
2026-06-17

---

## Table of Contents
1. [Core Principles](#core-principles)
2. [Development Philosophy](#development-philosophy)
3. [Quality Standards](#quality-standards)
4. [Change Management](#change-management)
5. [Code Review Process](#code-review-process)
6. [Security Requirements](#security-requirements)
7. [Performance Standards](#performance-standards)
8. [Documentation Standards](#documentation-standards)

---

## Core Principles

### Architecture First
All implementation decisions must prioritize long-term architectural integrity over short-term convenience.

### Production-Ready Only
Every commit must be production-ready code. No placeholders, no temporary hacks.

### Documentation Required
Every architectural decision, business rule, and system change must be documented before or alongside implementation.

### Security by Design
Security considerations are mandatory in every feature design and implementation.

---

## Development Philosophy

### Clean Architecture
- Follow Clean Architecture principles
- Dependency inversion
- Separation of concerns
- Independence of framework

### SOLID Principles
- Single Responsibility Principle
- Open/Closed Principle
- Liskov Substitution Principle
- Interface Segregation Principle
- Dependency Inversion Principle

### YAGNI vs. Future-Proofing
- Implement only what is needed today
- Design for extensibility without over-engineering
- Document anticipated future needs in ADRs

---

## Quality Standards

### Code Quality
- Code should be self-documenting
- Prefer composition over inheritance
- Keep methods focused and small (<50 lines)
- Keep classes cohesive and focused

### Testing Requirements
- Unit tests required for business logic
- Integration tests required for external dependencies
- E2E tests required for critical user flows

### Code Review Checklist
- [ ] Architecture compliance
- [ ] Security considerations
- [ ] Performance implications
- [ ] Documentation updates
- [ ] Test coverage

---

## Change Management

### Branch Strategy
- `main` - Production ready
- `develop` - Integration branch
- `feature/*` - Feature development
- `hotfix/*` - Production emergency fixes
- `release/*` - Release preparation

### Commit Conventions
Follow conventional commits format:
- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation only
- `refactor:` - Code refactoring
- `test:` - Adding tests
- `chore:` - Maintenance tasks

---

## Code Review Process

### Mandatory Reviews
- Minimum 1 reviewer for features
- Minimum 2 reviewers for architecture changes
- All CI/CD checks must pass

### AI Assistant Review Protocol
All AI-generated code must be reviewed against:
- Security vulnerabilities
- Performance issues
- Architecture compliance
- Coding standards adherence

---

## Security Requirements

### Authentication
- Multi-factor authentication for sensitive operations
- JWT tokens with appropriate expiration
- Secure session management

### Authorization
- Role-based access control (RBAC)
- Principle of least privilege
- Resource-level authorization

### Data Protection
- Encryption at rest and in transit
- PII data handling compliance
- Audit logging for sensitive operations

---

## Performance Standards

### API Response Times
- P95 < 200ms for read operations
- P95 < 500ms for write operations
- P99 < 1000ms maximum

### Scalability Targets
- Horizontal scaling capability
- Stateless services where possible
- Caching strategy for read-heavy operations

---

## Documentation Standards

### Living Documentation
All documentation must be version-controlled alongside code.

### Architecture Documentation
Stored in `/docs` directory with clear structure.

### Decision Records
All significant architectural decisions documented in `/adr`.

---

## Decision Protocol

### Documentation as Source of Truth
Requirements and documentation in `/docs` are the authoritative source. No technologies, frameworks, or implementation details are assumed without explicit documentation.

### Missing Decision Handling
When a required technical decision lacks documentation:
1. Identify the missing decision
2. Document in a task file or request clarification
3. Wait for explicit user approval before proceeding
4. Only then create ADR or implement

### ADR Creation Policy
Architecture Decision Records may only be created after a technical decision has been officially approved. ADRs are documentation, not proposal tools.

---

## Sections Prepared for Future Content

### Team Structure
*To be defined*

### Onboarding Process
*To be defined*

### Incident Response
*To be defined*

### Release Process
*To be defined*