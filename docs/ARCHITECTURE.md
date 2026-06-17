# System Architecture

## Purpose
Documents the complete system architecture, technology stack, and design decisions for GymPlatform.

## Scope
All system components, integration patterns, deployment architecture, and technology choices.

## Owner
Lead Software Architect

## Status
Draft - Under Review

## Last Updated
2026-06-17

> **Note:** This document contains preliminary architecture considerations. All technology choices are subject to formal approval and should not be considered final architectural decisions.

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
Clean Architecture (also known as Onion Architecture or Hexagonal Architecture) with clear separation of concerns.

### Deployment Model
- Cloud-native, multi-tenant SaaS
- Micro-frontends for independent deployment
- Container-based microservices (future evolution)

### Key Design Principles
- **SOLID** - All code follows SOLID principles
- **DRY** - Don't Repeat Yourself
- **KISS** - Keep It Simple, Stupid
- **YAGNI** - You Aren't Gonna Need It
- **Fail Fast** - Early validation and error detection

### Architectural Constraints
- Domain layer has no external dependencies
- Application layer depends only on Domain
- Infrastructure implements Domain interfaces
- API is orchestration only

---

## Technology Stack

### Backend (.NET)
| Layer | Technology | Version |
|-------|------------|---------|
| Framework | ASP.NET Core | 8.0+ |
| Language | C# | 12+ |
| ORM | Entity Framework Core | 8.0+ |
| Messaging | RabbitMQ | 3.12+ or Azure Service Bus |
| Database | PostgreSQL | 16+ |
| Caching | Redis | 7.0+ |
| Authentication | IdentityServer or Azure AD B2C | latest |

### Frontend (Web)
| Layer | Technology | Version |
|-------|------------|---------|
| Framework | Next.js | 15+ |
| Runtime | React | 19+ |
| Language | TypeScript | 5+ |
| Styling | Tailwind CSS | 3+ |
| State Management | Zustand | latest |
| Data Fetching | TanStack Query | 5+ |
| Testing | Jest + React Testing Library | latest |

### Mobile
| Layer | Technology | Version |
|-------|------------|---------|
| Framework | React Native | 0.74+ |
| Navigation | React Navigation | 7+ |
| State | Redux Toolkit | latest |
| Storage | WatermelonDB | latest |

### Infrastructure
| Component | Technology |
|-----------|------------|
| Cloud | Azure |
| Container | Docker + Kubernetes |
| IaC | Terraform |
| CI/CD | GitHub Actions |
| Monitoring | Prometheus + Grafana |
| Logging | Elasticsearch |

---

## System Components

### Backend Services
```
GymPlatform.Api           # REST API endpoints
GymPlatform.Application   # Use cases and business logic
GymPlatform.Domain        # Entities, value objects, rules
GymPlatform.Infrastructure # External implementations
GymPlatform.SharedKernel  # Cross-cutting concerns
```

### Frontend Applications
```
web-app/                  # Next.js web application
mobile-app/               # React Native application
admin-portal/             # Admin dashboard
trainer-app/              # Trainer-specific interface
```

### Shared Components
```
design-system/            # UI component library
shared-utils/             # Cross-platform utilities
api-client/               # Generated API clients
```

---

## Data Architecture

### Database Schema
- PostgreSQL with one database per tenant
- Redis for session and caching
- S3-compatible storage for documents

### Data Flow
```
Client Request
    ↓
API Gateway
    ↓
Load Balancer
    ↓
Application Services
    ↓
Domain Logic
    ↓
Repository Pattern
    ↓
Database/Cache
```

### Event-Driven Integration
- Domain events for business actions
- Integration events for external systems
- Event sourcing for audit trail

### Data Privacy
- PII encrypted at rest
- Soft delete for all entities
- GDPR-compliant data export

---

## API Architecture

### API Layers
- **Public API** - External partner integrations
- **Internal API** - Web and mobile applications
- **Admin API** - Management and administrative functions

### API Patterns
- RESTful design principles
- CQRS for complex operations
- Rate limiting per client
- API versioning (v1, v2, etc.)

### Documentation
- OpenAPI 3.0 specification
- Swagger UI for interactive docs
- Postman collection for testing

---

## Security Architecture

### Authentication Flow
```
User Login
    ↓
Identity Provider (Azure AD B2C)
    ↓
JWT Token Generation
    ↓
Token to Client
    ↓
Client Stores in httpOnly Cookie
    ↓
Token Attached to Requests
    ↓
API Validates Token
```

### Authorization Model
- RBAC with Role-based policies
- Resource-level permissions
- Claims-based authorization

### Security Controls
- WAF for web protection
- DDoS protection
- TLS 1.3 everywhere
- Secrets management via Key Vault

---

## Deployment Architecture

### Environments
- **Development** - Local development
- **Staging** - Pre-production testing
- **Production** - Live customer traffic

### Infrastructure Regions
- Primary region for active service
- Secondary region for disaster recovery
- CDN for global content delivery

### Scaling Strategy
- Horizontal pod autoscaling
- Database read replicas
- Redis clustering for scale

---

## Monitoring and Observability

### Four Golden Signals
- Latency - Request duration
- Traffic - Request volume
- Errors - Failure rate
- Saturation - Resource utilization

### Telemetry Stack
- OpenTelemetry for instrumentation
- Prometheus for metrics
- Grafana for dashboards
- ELK for log aggregation

### Alerting
- SLO-based alerting
- PagerDuty integration
- On-call rotation schedules

---

## Sections Prepared for Future Content

### Service Mesh Configuration
*To be defined*

### Database Sharding Strategy
*To be defined*

### Event Schema Registry
*To be defined*

### Migration to Microservices
*To be defined*