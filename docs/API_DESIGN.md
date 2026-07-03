# API Design

## Purpose
Documents the REST API design for GymPlatform, covering current implementation patterns and standards based on the .NET 10 Minimal API architecture.

## Scope
All external API endpoints, request/response patterns, authentication, error handling, and implementation standards.

## Owner
Backend Architecture Lead

## Status
Active — Updated 2026-07-03

## Last Updated
2026-07-03

> **Note**: This document reflects current implementation. For conceptual API architecture and future strategy, refer to `.ai/context/API_BLUEPRINT.md`.

---

## Table of Contents
1. [API Principles](#api-principles)
2. [Technology](#technology)
3. [Endpoint Organization](#endpoint-organization)
4. [Authentication](#authentication)
5. [Request/Response Standards](#requestresponse-standards)
6. [Error Handling](#error-handling)
7. [Implementation Pattern](#implementation-pattern)

---

## API Principles

- **Resource-Oriented**: Endpoints represent domain entities or domain actions
- **REST with RPC Elements**: Standard CRUD plus domain-specific actions (POST for operations)
- **Minimal APIs**: No controllers; endpoint mapping in `Program.cs`
- **Consistent**: Uniform patterns across all endpoints
- **JSON:API Compliance**: Response envelope with `data`, `meta`, `links`
- **Idempotent**: Idempotency keys required for all write operations

---

## Technology

| Component | Technology | Version |
|-----------|-----------|---------|
| Framework | ASP.NET Core Minimal APIs | 10 |
| Documentation | Swashbuckle / Swagger | 8.1.0 |
| Authentication | JWT Bearer | Custom |
| Error Format | RFC 7807 ProblemDetails | Built-in |
| Async | Task-based async pattern | C# 12 |

---

## Endpoint Organization

### Current Implemented Endpoints

**Membership (4 endpoints)**:
```
POST /api/gyms                        — Create new gym
POST /api/members                     — Register new member
POST /api/coaches/{coachId}/assign    — Assign coach to member
POST /api/gyms/{gymId}/deactivate     — Deactivate gym
```

**Training (7 endpoints)**:
```
POST /api/exercises                   — Create exercise
POST /api/workout-programs            — Create workout program
POST /api/workout-logs                — Log workout completion
POST /api/exercise-videos             — Upload exercise video
POST /api/body-measurements           — Record body measurement
POST /api/progress-photos             — Upload progress photo
PATCH /api/coach-profiles             — Update coach profile
```

**Communication Calendar (6 endpoints)**:
```
POST /api/rooms                       — Create room
POST /api/sessions                    — Create session (with overlap validation)
POST /api/bookings                    — Book session for member
POST /api/sessions/{sessionId}/cancel — Cancel session
POST /api/bookings/{bookingId}/cancel — Cancel booking
POST /api/coaches/{coachId}/availability — Set coach availability
```

### Full Endpoint Catalog (Planned)

| Module | Endpoint Group | Phase |
|--------|----------------|-------|
| Membership | `/api/gyms`, `/api/members`, `/api/coaches` | Phase 0 ✅ |
| Training | `/api/exercises`, `/api/workout-programs`, `/api/workout-logs`, `/api/exercise-videos`, `/api/body-measurements`, `/api/progress-photos`, `/api/coach-profiles` | Phase 1 ✅ |
| Communication | `/api/rooms`, `/api/sessions`, `/api/bookings`, `/api/coaches/{id}/availability` | Phase 2 🔄 |
| Chat & Messaging | `/api/conversations`, `/api/messages` | Phase 2 📋 |
| Notifications | `/api/notifications` | Phase 2 📋 |
| Calendar | `/api/events` | Phase 2 📋 |
| Settings | `/api/settings` | Phase 3 📋 |
| Admin | `/api/admin/*` | Phase 3 📋 |
| Media Management | `/api/media` | Phase 3 📋 |
| Reports & Analytics | `/api/reports`, `/api/dashboards` | Phase 3 📋 |
| Payments | `/api/payment-methods`, `/api/subscriptions` | Phase 4 📋 |
| Financial | `/api/financial/*` | Phase 4 📋 |
| Marketplace | `/api/marketplace/*` | Phase 4 📋 |
| Reviews & Ratings | `/api/reviews` | Phase 4 📋 |

---

## Authentication

### JWT Token Structure

```
Header:  { "alg": "HS256", "typ": "JWT" }
Payload: {
  "sub": "user-guid",           // User ID
  "tenant": "gym-guid",         // Tenant ID (gym)
  "roles": ["GymOwner"],        // User roles
  "permissions": ["members:read", "members:write"],  // Granular permissions
  "exp": 1516239022,            // Expiration timestamp
  "iat": 1516239022,            // Issued at timestamp
  "jti": "unique-token-id"      // JWT ID for revocation
}
```

### Token Acquisition

1. Client authenticates via `/auth/login`
2. Server validates credentials
3. JWT access token (15 min) + refresh token (7 days) returned
4. Access token included in `Authorization: Bearer <token>` header
5. On 401, client uses refresh token via `/auth/refresh`
6. New access token returned without re-authentication

### Current Authentication (Minimal API)

```csharp
// GymPlatform.Api/Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
```

---

## Request/Response Standards

### Request Format

```csharp
// POST /api/members
{
  "gymId": "guid",
  "fullName": "علی محمدی",
  "email": "ali@example.com",
  "phone": "+989123456789"
}
```

### Response Format (Success)

```json
{
  "data": {
    "id": "guid",
    "fullName": "علی محمدی",
    "email": "ali@example.com",
    "status": "Active",
    "createdAt": "2026-06-29T00:00:00Z"
  }
}
```

### Idempotency Key

All write operations (POST, PATCH, DELETE) require:
```
Idempotency-Key: <unique-uuid>
```

### Pagination (Planned for List Endpoints)

```
GET /api/members?page=1&pageSize=20

Response:
{
  "data": [...],
  "meta": {
    "page": 1,
    "pageSize": 20,
    "totalItems": 150,
    "totalPages": 8
  }
}
```

---

## Error Handling

### Error Response Format (RFC 7807 ProblemDetails)

```json
{
  "type": "https://httpstatuses.com/400",
  "title": "Validation Error",
  "status": 400,
  "detail": "Email format is invalid",
  "instance": "/api/members",
  "errors": {
    "email": ["Required", "Invalid format"]
  }
}
```

### HTTP Status Codes

| Status | Meaning | Usage |
|--------|---------|-------|
| 200 OK | Success | GET, successful PATCH |
| 201 Created | Resource created | POST |
| 400 Bad Request | Validation error | Invalid input |
| 401 Unauthorized | Not authenticated | Missing/invalid token |
| 403 Forbidden | Not authorized | Valid token, insufficient permissions |
| 404 Not Found | Resource not found | Entity doesn't exist or inaccessible |
| 409 Conflict | State conflict | Business rule violation |
| 422 Unprocessable Entity | Business rule failure | Domain validation failure |
| 429 Too Many Requests | Rate limited | Rate limit exceeded |
| 500 Internal Server Error | Server error | Unhandled exception |

### Error Handling Middleware

```csharp
// GymPlatform.Api/GlobalExceptionMiddleware.cs
public async Task InvokeAsync(HttpContext context)
{
    try
    {
        await _next(context);
    }
    catch (Exception ex)
    {
        await HandleExceptionAsync(context, ex);
    }
}
```

---

## Implementation Pattern

### Standard Endpoint Implementation

```csharp
// Program.cs pattern
app.MapPost("/api/rooms", async (
    CreateRoomCommand command,
    ICommandValidator<CreateRoomCommand> validator,
    ICommandHandler<CreateRoomCommand, RoomResponse> handler,
    CancellationToken ct) =>
{
    // Validate
    var validation = validator.Validate(command);
    if (validation.IsFailure)
        return Results.Problem(statusCode: 400, detail: validation.Error);

    // Handle
    var result = await handler.HandleAsync(command, ct);
    if (result.IsFailure)
        return Results.Problem(statusCode: 400, detail: result.Error);

    // Return
    return Results.Ok(result.Value);
})
.RequireAuthorization()
.WithName("CreateRoom")
.WithTags("Rooms");
```

### Registration Pattern

```csharp
// Registration in Program.cs
builder.Services.AddScoped<ICommandHandler<CreateRoomCommand, RoomResponse>,
    CreateRoomCommandHandler>();
builder.Services.AddScoped<ICommandValidator<CreateRoomCommand>,
    CreateRoomCommandValidator>();
```

---

*End of API Design — 2026-07-03*
