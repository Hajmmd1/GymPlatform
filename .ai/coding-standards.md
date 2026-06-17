# Coding Standards

## Purpose
Establishes consistent coding conventions, best practices, and quality expectations for all GymPlatform code contributors.

## Scope
Backend (C#/.NET), Frontend (TypeScript/React), Mobile (React Native), and Infrastructure (Terraform) codebases.

## Owner
Principal Software Engineer

## Status
Draft

## Last Updated
2026-06-17

---

## Table of Contents
1. [General Principles](#general-principles)
2. [Naming Conventions](#naming-conventions)
3. [Backend Standards (C#)](#backend-standards-c)
4. [Frontend Standards (TypeScript/React)](#frontend-standards-typescriptreact)
5. [Mobile Standards (React Native)](#mobile-standards-react-native)
6. [Infrastructure Standards (Terraform)](#infrastructure-standards-terraform)
7. [Testing Standards](#testing-standards)
8. [Code Quality Gates](#code-quality-gates)

---

## General Principles

### Code Organization
- One class per file
- Related functionality grouped in namespaces/packages
- No file exceeding 500 lines

### Comments Policy
- Code should be self-documenting
- Comments explain "why", not "what"
- XML documentation for public APIs (C#)
- JSDoc for public functions (TypeScript)

### Version Control
- Atomic commits
- Descriptive commit messages
- Feature branches with clear naming

---

## Naming Conventions

### Files and Directories
- PascalCase for C# files
- kebab-case for TypeScript/JavaScript files
- Descriptive, noun-based naming

### Variables and Functions
- camelCase for local variables and functions
- PascalCase for classes and interfaces
- UPPER_SNAKE_CASE for constants

### Database
- snake_case for table names and columns
- Primary keys: `{table_name}_id`
- Foreign keys: `{referenced_table}_id`

---

## Backend Standards (C#)

### Language Version
- C# 12+ features permitted
- Nullable reference types enabled
- Implicit usings disabled (explicit is clearer)

### Project Structure
```
/src
  /GymPlatform.Api
  /GymPlatform.Application
  /GymPlatform.Domain
  /GymPlatform.Infrastructure
  /GymPlatform.SharedKernel
/tests
  /GymPlatform.UnitTests
  /GymPlatform.IntegrationTests
```

### Code Style
- Braces on separate lines (Allman style)
- 4-space indentation
- `var` avoided, explicit types preferred
- Async/await for all I/O operations

### Design Patterns
- Repository pattern for data access
- Mediator pattern for commands/queries
- CQRS for complex business operations
- Dependency Injection throughout

---

## Frontend Standards (TypeScript/React)

### Language Features
- TypeScript strict mode enabled
- Latest ES2024 features
- Functional components with hooks
- Server Components where applicable

### Project Structure (Next.js)
```
/src
  /app          # App Router pages
  /components   # Reusable UI components
  /lib          # Utility functions and clients
  /hooks        # Custom React hooks
  /types        # TypeScript definitions
/public         # Static assets
```

### Component Design
- One component per file
- Props interface defined explicitly
- Compound components for complex UI
- Custom hooks for shared logic

### State Management
- React Context for simple state
- Zustand or Redux for complex state
- Server state via TanStack Query

---

## Mobile Standards (React Native)

### Project Structure
```
/src
  /components   # Shared mobile components
  /screens      # Screen components
  /navigation   # Navigation configuration
  /hooks        # Mobile-specific hooks
  /services     # Native service wrappers
/ios            # iOS native modules
/android        # Android native modules
```

### Platform Considerations
- Platform-agnostic code where possible
- Native modules for platform-specific features
- Offline-first architecture support

---

## Infrastructure Standards (Terraform)

### File Organization
```
/infra
  /modules      # Reusable Terraform modules
  /environments # Environment-specific configurations
  /global       # Global infrastructure resources
```

### Naming
- Resource names with project prefix
- Descriptive, purpose-based naming
- No hardcoded values, use variables

---

## Testing Standards

### Unit Tests
- Arrange-Act-Assert pattern
- Mock external dependencies
- Test one thing per test method

### Integration Tests
- Test database in Docker container
- Test API endpoints via HttpClient
- Clean up test data

### E2E Tests
- Critical user flows only
- Resilient selectors
- Parallel execution

---

## Code Quality Gates

### Static Analysis
- SonarAnalyzer for C#
- ESLint + Prettier for TypeScript
- Zero warnings policy

### Complexity Limits
- Cyclomatic complexity < 10
- File length < 500 lines
- Method length < 50 lines

### Code Coverage
- 80% minimum for business logic
- 70% minimum for infrastructure code
- Critical paths must have 100% coverage

---

## Sections Prepared for Future Content

### Refactoring Guidelines
*To be defined*

### Code Review Checklist
*To be defined*

### Performance Optimization
*To be defined*