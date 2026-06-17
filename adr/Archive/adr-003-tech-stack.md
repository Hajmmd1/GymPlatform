# ADR-003: .NET 8 and Next.js 15 Stack

## Status
Draft - Unapproved (Archived for Reference Only)

## Date
2026-06-17

## Note
This ADR was created during project initialization without formal architectural approval. It is archived for reference only and does NOT represent an official architecture decision.

## Context
GymPlatform requires a modern technology stack for backend and frontend development. Requirements:
- Long-term support and stability
- Strong typing for maintainability
- Cross-platform compatibility
- Performance and scalability
- Rich ecosystem for rapid development
- Team skill availability

Backend alternatives considered:
- **Node.js/Express** - Single language but less mature typing
- **Python/Django** - Rapid development but scaling concerns
- **Java/Spring Boot** - Enterprise-grade but verbose
- **Go/Fiber** - Performance but smaller ecosystem

Frontend alternatives considered:
- **Vue.js/Nuxt** - Good but smaller community than React
- **Svelte/SvelteKit** - Innovative but less mature
- **Angular** - Full-featured but opinionated and heavy
- **Vanilla JS/HTMX** - Lightweight but less scalable for team

## Decision
Use .NET 8 for backend and Next.js 15 for frontend.

Backend stack:
- ASP.NET Core 8 (LTS)
- C# 12
- Entity Framework Core
- IdentityServer for authentication
- CQRS with MediatR

Frontend stack:
- Next.js 15 (App Router)
- React 19 with Server Components
- TypeScript 5 (strict mode)
- Tailwind CSS
- TanStack Query

Mobile stack:
- React Native 0.74
- Expo SDK 51

## Consequences

### Positive
- .NET 8 is LTS with 3-year support
- C# is mature with excellent tooling (Rider, VS)
- Next.js has excellent performance and SEO
- TypeScript catches errors at compile time
- Single language paradigm (C# and TS both C-family)
- Strong ecosystem for both stacks
- Microsoft and Vercel backing for long-term viability

### Negative
- Two different runtimes to maintain
- Context switching between C# and TypeScript
- .NET on Azure may have vendor lock-in
- React Native has native module complexity

### Neutral
- Team may need training on modern patterns
- Stack will evolve but maintain backward compatibility