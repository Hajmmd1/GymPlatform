# Backend Rules

## Purpose
Defines backend-specific architecture, implementation, and operational guidelines for GymPlatform services.

## Scope
All backend services, APIs, data access layers, and infrastructure components.

## Owner
Backend Architecture Lead

## Status
Draft

## Last Updated
2026-06-17

---

## Table of Contents
1. [Technology Stack](#technology-stack)
2. [Architecture Patterns](#architecture-patterns)
3. [API Design Standards](#api-design-standards)
4. [Data Access Patterns](#data-access-patterns)
5. [Error Handling](#error-handling)
6. [Security Requirements](#security-requirements)
7. [Performance Requirements](#performance-requirements)
8. [Deployment Standards](#deployment-standards)

---

## Technology Stack

### Primary Framework
- .NET 8+ (ASP.NET Core)
- C# 12+ with nullable reference types
- Primary language: English

### Database Technology
- PostgreSQL (primary relational store)
- Redis (caching and session state)
- RabbitMQ or Azure Service Bus (messaging)

### Cloud Platform
- Azure (primary target)
- Kubernetes for orchestration
- Azure SQL Database for managed database

---

## Architecture Patterns

### Clean Architecture Enforcement
- Domain layer has zero dependencies
- Application layer depends on Domain
- Infrastructure layer implements interfaces from Application/Domain
- API layer orchestrates, contains no business logic

### CQRS Pattern
```
Commands: Write operations, return void
Queries: Read operations, return data
Command Handlers: Contain business logic for writes
Query Handlers: Contain logic for reads
```

### Mediator Pattern
- MediatR for in-process messaging
- Decouples controllers from handlers
- Enables pipeline behaviors

### Repository Pattern
- Abstract data access behind interfaces
- Specification pattern for queries
- Unit of Work for transaction boundaries

---

## API Design Standards

### RESTful Conventions
- Plural nouns for resource endpoints
- GET for read, POST for create, PUT for update, DELETE for delete
- Proper HTTP status codes (200, 201, 400, 401, 403, 404, 500)

### Versioning Strategy
- URL versioning: `/api/v1/resource`
- Swagger/OpenAPI documentation required
- Backward compatibility within major versions

### Request/Response Patterns
- Envelope pattern for all responses
- Consistent error response format
- Pagination for list endpoints

---

## Data Access Patterns

### Entity Framework Core
- Code-first migrations
- Never call DbContext directly from controllers
- Specification pattern for complex queries

### Soft Delete
- All entities include `IsDeleted` flag
- Global query filter for soft-deleted records
- Audit trail maintained

### Concurrency Control
- Optimistic concurrency with version tokens
- ETag headers for API concurrency

---

## Error Handling

### Exception Policies
- Never catch and swallow exceptions
- Domain exceptions for business rule violations
- Infrastructure exceptions for technical failures

### Global Exception Handler
- Returns Problem Details (RFC 7807)
- Sanitizes error messages for production
- Logs full exception details

### Validation
- FluentValidation for input validation
- Fail-fast validation in command handlers
- Domain validation in entities

---

## Security Requirements

### Authentication
- JWT Bearer tokens with refresh token rotation
- Token expiration: 15 minutes access, 7 days refresh
- Token revocation support

### Authorization
- Policy-based authorization
- Role-based claims in tokens
- Resource-level permissions

### Input Sanitization
- All inputs validated at entry point
- SQL injection prevention via EF Core
- XSS prevention via output encoding

---

## Performance Requirements

### Response Time Targets
- P95: < 200ms for reads
- P95: < 500ms for writes
- P99: < 1000ms maximum

### Caching
- Redis distributed cache
- Cache-aside pattern
- Cache invalidation on writes

### Database Optimization
- Indexes on all foreign keys
- Read replicas for reporting queries
- Connection pooling configured

---

## Deployment Standards

### Health Checks
- `/health` endpoint for liveness
- `/ready` endpoint for readiness
- Dependency health checks included

### Configuration
- Environment variables for secrets
- appsettings.json for defaults
- Feature flags via Azure App Configuration

### Observability
- OpenTelemetry instrumentation
- Structured JSON logging
- Metrics for business-critical operations

---

## Sections Prepared for Future Content

### Service Mesh Configuration
*To be defined*

### Database Migration Strategy
*To be defined*

### Backup and Recovery
*To be defined*

### Disaster Recovery
*To be defined*