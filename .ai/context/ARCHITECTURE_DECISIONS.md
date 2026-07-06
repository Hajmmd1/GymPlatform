# Architecture Decisions

This document contains active and proposed architectural decisions for GymPlatform. Each ADR is dated and includes context, options considered, decision, and consequences.

---

# ADR-VALIDATION-001: Command Validation Invocation Strategy

## Status
**Proposed** - Requires technical approval before implementation

## Date
2026-07-05

## Context

During investigation of the Communication module test failures (CS1729 constructor errors), a deeper analysis revealed inconsistent validation patterns across all modules:

### Current State Analysis

**Training Module (7 handlers):**
- All handlers inject `ICommandValidator<T>` via constructor
- All call `_validator.Validate()` inside `HandleAsync()` before business logic
- Validators registered in DI and invoked correctly at runtime
- Tests are consistent with implementation

**Membership Module (4 handlers):**
- `CreateGymCommandHandler`: Injects validator | Calls validator
- `RegisterMemberCommandHandler`: No validator injection | No validation call (validates via domain value objects `Email`, `Phone`)
- `DeactivateGymCommandHandler`: No validator injection | No validation call
- `AssignMemberToCoachCommandHandler`: No validator injection | No validation call
- Validators for non-injected commands ARE dead code (exists in source, but never invoked)
- Tests are internally consistent (separate validator tests, no validator in handler tests)

**Communication Module (6 handlers):**
- All handlers lack validator injection
- All validators are dead code (registered in DI but never invoked)
- Tests are BROKEN - attempt to construct handlers with validators that don't exist

### The Risk

This inconsistency creates a **structural bug risk**: a developer adding a new command handler has no enforced pattern. They might:
1. Create a validator but forget to inject it (becomes dead code)
2. Inject it but forget to call it (becomes dead code)
3. Not create a validator at all

The current codebase already demonstrates bugs #1 and #2 exist in production.

## Options Considered

### (a) Validator Injected into Handler and Called Inside `HandleAsync`

```csharp
public class CreateRoomCommandHandler : ICommandHandler<CreateRoomCommand, RoomResponse>
{
    private readonly ICommandValidator<CreateRoomCommand> _validator;
    
    public CreateRoomCommandHandler(IRoomRepository repo, ICommunicationUnitOfWork uow, ICommandValidator<CreateRoomCommand> validator)
    {
        _validator = validator;
    }
    
    public async Task<Result<RoomResponse>> HandleAsync(CreateRoomCommand command, CancellationToken ct)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
            return Result<RoomResponse>.Failure(validationResult.Error);
        // ... rest of logic
    }
}
```

| Criterion | Assessment |
|-----------|------------|
| Testability | Business logic and validation can be tested independently; handlers require validator in test setup |
| Safety/Forgiveness | Developer must remember to inject AND call validator — easy to forget one step |
| Clean Architecture fit | Validation is application layer concern; reasonable location |
| Microservices migration | Validation travels with the handler; portable across service boundaries |

### (b) Validator Invoked by Cross-Cutting Pipeline/Decorator

```csharp
// Generic decorator applied automatically to all ICommandHandler<T>
public class ValidationPipeline<TCommand, TResult> : ICommandHandler<TCommand, TResult> 
    where TCommand : ICommand
{
    private readonly ICommandHandler<TCommand, TResult> _handler;
    private readonly ICommandValidator<TCommand> _validator;
    
    public async Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken ct)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
            return Result<TResult>.Failure(validationResult.Error);
        return await _handler.HandleAsync(command, ct);
    }
}
```

| Criterion | Assessment |
|-----------|------------|
| Testability | Handlers can be tested in pure isolation; validation tested separately via pipeline tests |
| Safety/Forgiveness | **Cannot be forgotten** — automatically applied if validator exists; enforced by DI registration |
| Clean Architecture fit | Validation is cross-cutting concern; aligns with Decorator pattern |
| Microservices migration | Pipeline pattern is standard in MediatR and other mediator implementations; portable |

### (c) Validator Invoked at API/Endpoint Layer Before Handler

```csharp
app.MapPost("/api/rooms", async (
    [FromBody] CreateRoomRequest request,
    [FromServices] ICommandValidator<CreateRoomCommand> validator,
    [FromServices] ICommandHandler<CreateRoomCommand, RoomResponse> handler) =>
{
    var command = new CreateRoomCommand(request.GymId, request.Name, request.Capacity);
    var validationResult = validator.Validate(command);
    if (validationResult.IsFailure)
        return Results.BadRequest(validationResult.Error);
    // ... call handler
});
```

| Criterion | Assessment |
|-----------|------------|
| Testability | Business logic can be tested in pure isolation; validation is endpoint concern |
| Safety/Forgiveness | **Cannot be forgotten** per-endpoint, but requires duplicate endpoint changes; easy to miss new endpoints |
| Clean Architecture fit | Mixes presentation (endpoint) with application layer (validation) — violates layer separation |
| Microservices migration | Tightly couples validation to HTTP layer; not portable to message handlers, gRPC, etc. |

## Decision

**Option (b) - Cross-Cutting Pipeline/Decorator** is selected as the mandatory project-wide standard.

### Justification

1. **Structural Safety**: The pipeline pattern makes correct behavior the *path of least resistance*. If a validator exists and is registered, it WILL be invoked automatically. This prevents the Communication module's exact bug class.

2. **Forgiveness Over Remembering**: With 4 modules and dozens of future commands, relying on developers to "remember" to inject and call validators is a failed strategy. The current codebase proves this.

3. **Clean Architecture Alignment**: The decorator pattern is explicitly recommended in Clean Architecture for cross-cutting concerns. Validation is not domain logic (belongs in domain value objects) and not business use-case logic (belongs in handlers) — it is infrastructure/adjunct.

4. **Transport Agnostic**: The same pipeline works for HTTP endpoints, message queue handlers, gRPC, and future transport mechanisms. Critical for a future microservices architecture where commands may flow from multiple sources.

5. **Migration Simplicity**: Moving from Training's explicit pattern to pipeline is mechanical — remove explicit calls, add validator registration, handlers become cleaner.

### Long-Term Consequences

**Positive:**
- New command handlers automatically get validation if validators are registered
- Cannot accidentally ship unvalidated commands
- Handlers are cleaner with less boilerplate
- Validation is consistently testable via pipeline integration tests
- Financial module and future modules (HR, Marketplace, etc.) have a single enforced pattern

**Negative:**
- Adds complexity: pipeline registration and understanding required
- Debugging validation failures requires tracing through decorator chain
- Training module currently working pattern must be refactored (low risk, mechanical change)

**Neutral:**
- MediatR-style pipelines are industry standard
- Team learning curve for pipeline concepts

## Migration Plan

### Phase 1: Foundation
1. Create `ValidationPipeline<TCommand, TResult>` in SharedKernel or common Infrastructure
2. Add registration for pipeline in DI container
3. Write pipeline integration tests

### Phase 2: Communication Module (Priority - Broken Tests)
1. `CreateRoomCommandHandler`: Add validator injection, call `_validator.Validate()` at start
2. `CreateSessionCommandHandler`: Add validator injection, call `_validator.Validate()` at start
3. `BookSessionCommandHandler`: Add validator injection, call `_validator.Validate()` at start
4. `CancelBookingCommandHandler`: Add validator injection, call `_validator.Validate()` at start
5. `CancelSessionCommandHandler`: Add validator injection, call `_validator.Validate()` at start
6. `SetCoachAvailabilityCommandHandler`: Add validator injection, call `_validator.Validate()` at start
6. Fix tests to properly construct handlers with validator
7. Refactor to use pipeline (remove explicit validation calls)

### Phase 3: Training Module (Refactor to Standardize)
1. Remove explicit `_validator.Validate()` calls from all 7 handlers
2. Remove `_validator` field and constructor parameter from all handlers
3. Verify handlers still work via existing pipeline tests
4. Validators remain registered in DI, invoked by pipeline instead

### Phase 4: Membership Module (Inconsistency Resolution)
1. Audit `RegisterMemberCommandValidator` - may be redundant since `Email` and `Phone` value objects throw on invalid format
2. Audit `DeactivateGymCommandValidator` and `AssignMemberToCoachCommandValidator` - currently dead code
3. For validators that should exist: Register in DI (CreateGym already registered)
4. For `CreateGymCommandHandler`: Refactor to use pipeline instead of explicit validation

### Phase 5: Financial Module (Future Work)
- New handlers MUST follow pipeline pattern (documented in coding standards)

---

## Related

- Code locations affected: `GymPlatform.Modules.Communication.Tests/Application/Commands/*/`, `GymPlatform.Modules.Training/Application/Commands/*/`
- Related ADRs: None (this is the first validation-related decision)