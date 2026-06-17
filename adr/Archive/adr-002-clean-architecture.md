# ADR-002: Clean Architecture Pattern

## Status
Draft - Unapproved (Archived for Reference Only)

## Date
2026-06-17

## Note
This ADR was created during project initialization without formal architectural approval. It is archived for reference only and does NOT represent an official architecture decision.

## Context
GymPlatform is a long-term enterprise SaaS expected to evolve over many years. The architecture must support:
- Easy testing of business logic
- Framework independence
- Database independence
- Clear separation of concerns
- Future migration to microservices
- Independent evolution of UI layers

Alternatives considered:
- **Traditional layered architecture** - Tightly coupled to frameworks
- **Hexagonal Architecture** - Similar to Clean, but different terminology
- **Vertical Slice Architecture** - Good for features but harder to enforce boundaries
- **Microservices from day one** - Premature optimization for unknown scale

## Decision
Adopt Clean Architecture (Onion Architecture) as the primary architectural pattern.

Layer structure:
```
┌─────────────────────────────────────────────────────────┐
│                     Presentation                        │
│   (Next.js Web, React Native Mobile, API Controllers)    │
├─────────────────────────────────────────────────────────┤
│                     Application                         │
│   (Use Cases, Commands, Queries, DTOs)                  │
├─────────────────────────────────────────────────────────┤
│                     Domain                            │
│   (Entities, Value Objects, Domain Services, Rules)      │
├─────────────────────────────────────────────────────────┤
│                     Infrastructure                    │
│   (Repositories, External APIs, Email, Messaging)        │
└─────────────────────────────────────────────────────────┘
```

## Consequences

### Positive
- Domain logic is framework-agnostic
- Easy to test without external dependencies
- Clear boundaries between concerns
- Can evolve to microservices later
- UI technology can change without affecting core logic

### Negative
- More boilerplate code initially
- Learning curve for team
- Potential over-engineering for simple features
- More projects/assemblies to manage

### Neutral
- Industry standard pattern with extensive documentation
- Teams familiar with this pattern report high satisfaction