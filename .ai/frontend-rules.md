# Frontend Rules

## Purpose
Defines frontend-specific architecture, implementation, and UX guidelines for the GymPlatform web application.

## Scope
Next.js frontend, React components, TypeScript code, and user interface standards.

## Owner
Frontend Architecture Lead

## Status
Draft

## Last Updated
2026-06-17

---

## Table of Contents
1. [Technology Stack](#technology-stack)
2. [Architecture Patterns](#architecture-patterns)
3. [Component Design](#component-design)
4. [State Management](#state-management)
5. [Routing and Navigation](#routing-and-navigation)
6. [Performance Requirements](#performance-requirements)
7. [Security Requirements](#security-requirements)
8. [Accessibility Standards](#accessibility-standards)
9. [Testing Strategy](#testing-strategy)

---

## Technology Stack

### Core Framework
- Next.js 15+ (App Router)
- React 19+ with Server Components
- TypeScript 5+ strict mode

### UI Library
- Tailwind CSS for styling
- Radix UI for accessible primitives
- React Hook Form for form management
- TanStack Query for server state

### Build Tools
- Turbopack (Next.js default)
- ESLint + Prettier for code quality
- Jest + React Testing Library for tests

---

## Architecture Patterns

### Component Hierarchy
```
/app
  /[feature]/page.tsx       # Page components (Server Components)
/components
  /[ui]/                    # Design system components
  /[feature]/               # Feature-specific components
  /layouts/                 # Layout components
/lib
  /api/                     # API client wrappers
  /utils/                   # Utility functions
  /hooks/                   # Custom hooks
```

### Server vs Client Components
- Server Components for data-fetching and static content
- Client Components for interactivity and state
- Minimize client bundle size

### Co-location Principle
- Components, styles, and tests in same directory
- Feature-based folder organization
- Clear module boundaries

---

## Component Design

### Presentational vs Container
- Presentational: Pure UI, no business logic
- Container: Data fetching, state management
- Clear separation of concerns

### Design System
- Storybook for component documentation
- Atomic design principles
- Theme configuration via CSS variables

### Composition Patterns
- Compound components for complex UI
- Render props for flexible composition
- Custom hooks for shared logic

---

## State Management

### Server State
- TanStack Query for async data
- Stale-while-revalidate caching
- Optimistic updates for mutations

### Client State
- Zustand for global state
- React Context for simple state
- localStorage for persistence where appropriate

### Form State
- React Hook Form for forms
- Zod for schema validation
- Controlled inputs preferred

---

## Routing and Navigation

### App Router Structure
- Route groups for feature organization
- Parallel routes for modal flows
- Intercepting routes for clean UX

### Navigation Patterns
- Programmatic navigation minimal
- Link components for declarative navigation
- Loading states for async transitions

---

## Performance Requirements

### Core Web Vitals
- LCP < 2.5s
- FID < 100ms
- CLS < 0.1

### Bundle Optimization
- Code splitting via dynamic imports
- Tree-shaking for unused code
- Image optimization via Next.js

### Caching Strategy
- SWR for client-side caching
- Next.js ISR for server caching
- CDN for static assets

---

## Security Requirements

### Authentication Flow
- JWT tokens stored in httpOnly cookies
- Token refresh via silent refresh
- Session management via auth context

### Content Security Policy
- Strict CSP headers
- XSS prevention via output encoding
- CSRF protection for mutations

### Input Validation
- Client-side validation for UX
- Server-side validation always enforced
- Sanitize user inputs

---

## Accessibility Standards

### WCAG 2.1 AA Compliance
- Semantic HTML elements
- ARIA labels where needed
- Keyboard navigation support

### Screen Reader Support
- Proper heading hierarchy
- Descriptive alt text for images
- Form labels properly associated

### Color Contrast
- Minimum 4.5:1 contrast ratio
- Color not sole indicator of state
- Focus indicators visible

---

## Testing Strategy

### Unit Tests
- Jest for component logic
- Testing Library for DOM interaction
- 80% coverage target

### Integration Tests
- API integration tests
- Form submission flows
- Authentication flows

### E2E Tests
- Playwright for browser tests
- Critical user journeys only
- Visual regression testing

---

## Sections Prepared for Future Content

### Internationalization Strategy
*To be defined*

### SEO Optimization Guidelines
*To be defined*

### Progressive Web App Features
*To be defined*

### Mobile Performance Optimization
*To be defined*