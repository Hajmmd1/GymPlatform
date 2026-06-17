# API Design

## Purpose
Documents REST API design principles, conventions, endpoints, and integration patterns for GymPlatform.

## Scope
All external and internal API endpoints, request/response schemas, and integration contracts.

## Owner
Backend Architecture Lead

## Status
Draft - Under Review

## Last Updated
2026-06-17

> **Note:** This document contains preliminary API design guidelines. Specific implementations and technologies are subject to formal approval.

---

## Table of Contents
1. [API Principles](#api-principles)
2. [Design Standards](#design-standards)
3. [Endpoint Conventions](#endpoint-conventions)
4. [Authentication](#authentication)
5. [Error Handling](#error-handling)
6. [Rate Limiting](#rate-limiting)
7. [Versioning](#versioning)
8. [Pagination](#pagination)
9. [Filtering and Sorting](#filtering-and-sorting)

---

## API Principles

### RESTful Design
- Resources identified by URIs
- Standard HTTP methods (GET, POST, PUT, DELETE, PATCH)
- Stateless operations
- Cacheable responses where appropriate

### API First
- OpenAPI specification written before implementation
- Contract testing with Pact or similar
- Documentation as discovery

### Security First
- HTTPS everywhere
- Input validation at entry point
- No sensitive data in responses

---

## Design Standards

### URL Structure
```
/api/v1/{resource}              # Collection
/api/v1/{resource}/{id}         # Item
/api/v1/{resource}/{id}/{sub}   # Sub-resource
```

### Resource Naming
- Plural nouns: `/memberships`, `/classes`, `/trainers`
- No verbs in URLs
- Lowercase with hyphens

### HTTP Methods
- GET - Retrieve resource(s)
- POST - Create resource
- PUT - Replace resource
- PATCH - Partial update
- DELETE - Delete resource

---

## Endpoint Conventions

### Collection Endpoints
```
GET    /api/v1/members         # List members
POST   /api/v1/members         # Create member
```

### Item Endpoints
```
GET    /api/v1/members/{id}    # Get specific member
PUT    /api/v1/members/{id}    # Replace member
PATCH  /api/v1/members/{id}    # Update member
DELETE /api/v1/members/{id}    # Delete member
```

### Action Endpoints
```
POST   /api/v1/members/{id}/checkin
POST   /api/v1/classes/{id}/book
POST   /api/v1/payments/{id}/refund
```

---

## Authentication

### OAuth 2.0 Flow
- Authorization Code flow for web
- PKCE flow for mobile
- Client Credentials for services

### Token Format
- JWT access tokens
- 15-minute expiration
- Refresh token rotation

### Token Storage
- httpOnly cookies (web)
- Secure storage (mobile)
- No localStorage for tokens

### Token Validation
- Validate signature, issuer, audience
- Check expiration
- Verify claims

---

## Error Handling

### Error Response Format
```json
{
  "type": "https://httpstatuses.com/400",
  "title": "Validation Error",
  "status": 400,
  "detail": "Email format is invalid",
  "instance": "/api/v1/members",
  "errors": {
    "email": ["Required", "Invalid format"]
  },
  "traceId": "00-abc123..."
}
```

### HTTP Status Codes
- 200 OK - Success
- 201 Created - Resource created
- 400 Bad Request - Validation error
- 401 Unauthorized - Not authenticated
- 403 Forbidden - Not authorized
- 404 Not Found - Resource not found
- 409 Conflict - State conflict
- 429 Too Many Requests - Rate limited
- 500 Internal Server Error - Server error

### Error Types
- Validation errors (400)
- Authentication errors (401)
- Authorization errors (403)
- Business rule errors (400/409)
- System errors (500)

---

## Rate Limiting

### Limits
- Authenticated: 1000 requests/hour
- Anonymous: 100 requests/hour
- Burst: 20 requests/second

### Headers
```
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 999
X-RateLimit-Reset: 2026-06-17T22:00:00Z
```

### Response (429)
```json
{
  "type": "https://httpstatuses.com/429",
  "title": "Too Many Requests",
  "detail": "Rate limit exceeded. Try again in 3600 seconds."
}
```

---

## Versioning

### URL Versioning
- `/api/v1/` - Current stable version
- `/api/v2/` - Next version (when needed)

### Compatibility
- Backward compatible within major version
- Deprecation headers in responses
- 6-month notice for breaking changes

### Version Lifecycle
- v1 - Currently supported
- v2 - Development
- v0 - Deprecated, removal planned

---

## Pagination

### Request Parameters
- `page` - Page number (default: 1)
- `pageSize` - Items per page (default: 20, max: 100)
- `sort` - Sort field and direction

### Response Headers
```
Link: </api/v1/members?page=2>; rel="next", </api/v1/members?page=5>; rel="last"
X-Total-Count: 100
X-Page-Count: 5
```

### Response Body
```json
{
  "data": [...],
  "pagination": {
    "page": 1,
    "pageSize": 20,
    "totalItems": 100,
    "totalPages": 5,
    "hasPrevious": false,
    "hasNext": true
  }
}
```

---

## Filtering and Sorting

### Filtering
- `?status=active` - Equality filter
- `?createdAfter=2026-01-01` - Date range
- `?name.contains=john` - Contains search

### Sorting
- `?sort=name` - Ascending
- `?sort=-name` - Descending
- `?sort=name,-created` - Multiple fields

---

## Sections Prepared for Future Content

### API Endpoint Catalog
*To be populated with all endpoints*

### Webhook Endpoints
*To be defined*

### GraphQL Schema (if applicable)
*To be defined*

### GraphQL Federation
*To be defined*