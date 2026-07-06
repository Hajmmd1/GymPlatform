# Technical Design: ADR-VALIDATION-001 Implementation

This document provides the concrete implementation design for ADR-VALIDATION-001. The ADR lives in `.ai/context/ARCHITECTURE_DECISIONS.md` and is the authoritative decision record.

---

## Critical Prerequisite: Interface Unification

The `ICommand`, `ICommandHandler<TCommand, TResult>`, and `ICommandValidator<TCommand>` interfaces are currently duplicated in three module namespaces:
- `GymPlatform.Modules.Membership.Application.Interfaces`
- `GymPlatform.Modules.Training.Application.Interfaces`
- `GymPlatform.Modules.Communication.Application.Interfaces`

These must be unified into `GymPlatform.SharedKernel` for a shared decorator to work.

---

## Concrete Implementation

### 1. New Files to Create in SharedKernel

**`GymPlatform.SharedKernel/Interfaces/ICommand.cs`**
```csharp
namespace GymPlatform.SharedKernel;

public interface ICommand<out TResult>
{
}
```

**`GymPlatform.SharedKernel/Interfaces/ICommandHandler.cs`**
```csharp
namespace GymPlatform.SharedKernel;

public interface ICommandHandler<in TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
```

**`GymPlatform.SharedKernel/Interfaces/ICommandValidator.cs`**
```csharp
namespace GymPlatform.SharedKernel;

public interface ICommandValidator<in TCommand>
{
    Result Validate(TCommand command);
}
```

**`GymPlatform.SharedKernel/Decorators/ValidatingCommandHandlerDecorator.cs`**
```csharp
namespace GymPlatform.SharedKernel;

public sealed class ValidatingCommandHandlerDecorator<TCommand, TResult> 
    : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _innerHandler;
    private readonly ICommandValidator<TCommand> _validator;

    public ValidatingCommandHandlerDecorator(
        ICommandHandler<TCommand, TResult> innerHandler,
        ICommandValidator<TCommand> validator)
    {
        _innerHandler = innerHandler ?? throw new ArgumentNullException(nameof(innerHandler));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
        {
            return Result<TResult>.Failure(validationResult.Error);
        }

        return await _innerHandler.HandleAsync(command, cancellationToken);
    }
}
```

### 2. NuGet Dependency Required

**Add to `GymPlatform.Api.csproj`:**
```xml
<PackageReference Include="Scrutor" Version="5.0.0" />
```

Scrutor provides the `Decorate()` extension method for generic DI decoration.

### 3. DI Registration Changes (Program.cs)

After interfaces are unified and Scrutor is added:

```csharp
// 1. Register concrete handlers (unchanged)
builder.Services.AddScoped<ICommandHandler<CreateGymCommand, GymResponse>, CreateGymCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateRoomCommand, RoomResponse>, CreateRoomCommandHandler>();
// ... all other handlers

// 2. Register validators (already done, just use SharedKernel interface)
builder.Services.AddScoped<ICommandValidator<CreateGymCommand>, CreateGymCommandValidator>();
builder.Services.AddScoped<ICommandValidator<CreateRoomCommand>, CreateRoomCommandValidator>();
// ... all validators

// 3. Add decorator registration (NEW - single line enables all)
builder.Services.Decorate(typeof(ICommandHandler<,>), typeof(ValidatingCommandHandlerDecorator<,>));
```

### 4. Handler Changes Required

All handlers must update their using statements from module-local to SharedKernel:

**Before (Membership):**
```csharp
using GymPlatform.Modules.Membership.Application.Interfaces; // ICommand, ICommandHandler, ICommandValidator
```

**After (all modules):**
```csharp
using GymPlatform.SharedKernel; // ICommand, ICommandHandler, ICommandValidator
```

Training module handlers must also remove explicit validator injection and calls:
- Remove `_validator` field
- Remove `ICommandValidator<T>` constructor parameter
- Remove `_validator.Validate(command)` call at start of `HandleAsync()`

---

## File Locations Summary

| Action | Files Affected |
|--------|---------------|
| Create new interfaces | `GymPlatform.SharedKernel/Interfaces/ICommand.cs`, `ICommandHandler.cs`, `ICommandValidator.cs` |
| Create decorator | `GymPlatform.SharedKernel/Decorators/ValidatingCommandHandlerDecorator.cs` |
| Add NuGet package | `GymPlatform.Api/GymPlatform.Api.csproj` |
| Update handler usings | All handler files in Membership, Training, Communication modules |
| Remove explicit validation (Training only) | All 7 Training module handlers |
| Register decorator in DI | `GymPlatform.Api/Program.cs` |

---

## Implementation Strategy Recommendation

### 1. Module Rollout Order

**Recommend: Module-by-module rollout** (NOT simultaneous change across all 17 handlers).

Using separate commits per module provides:
- **Smaller code reviews**: Communication module (6 handlers) is ~280 lines change; Training (7 handlers) is ~500 lines; Membership (4 handlers) is ~350 lines. Smaller PRs are more reviewable.
- **Lower risk to working code**: Training currently has working validation. Keeping it separate means we can verify the decorator works on Communication first (where there is zero working validation) before touching working Training code.
- **Easier rollback**: If the decorator has unexpected behavior, we can revert one module without affecting others.
- **Incremental verification**: Each module can have its own verification step.

**Rollout Order:**
1. **Communication First** - lowest regression risk (currently 0 working validators, broken tests)
2. **Training Second** - medium risk (working validators, must verify no behavioral change)
3. **Membership Third** - highest review complexity (mixed state: 1 working validator, 3 dead code validators)

### 2. Scrutor Dependency Re-evaluation

**Decision: DO NOT add Scrutor. Use manual registration.**

**Reasoning:**
1. **Existing codebase pattern**: Every handler is explicitly registered with `AddScoped<ICommandHandler<SpecificCommand, Result>, SpecificHandler>()`. Adding Scrutor's generic decoration would be the ONLY "magic" registration pattern in the entire codebase.

2. **No reflection-based bulk registration anywhere**: The codebase intentionally avoids reflection and bulk-registration patterns. This suggests a design philosophy of explicit, discoverable, and debuggable registrations.

3. **Manual registration is trivial here**: We already have explicit registrations; adding explicit decorator registrations alongside them is consistent:
   ```csharp
   // Handler registration
   builder.Services.AddScoped<ICommandHandler<CreateExerciseCommand, ExerciseResponse>, CreateExerciseCommandHandler>();
   
   // Decorator registration (one per command type, explicit like the handler)
   builder.Services.Decorate<ICommandHandler<CreateExerciseCommand, ExerciseResponse>, ValidatingCommandHandlerDecorator<CreateExerciseCommand, ExerciseResponse>>();
   ```

4. **Future maintainers can understand without learning Scrutor**: A developer reading the code can see exactly what wraps what without needing to understand generic type parameter mechanics of a decoration library.

5. **Zero new dependencies for a simple pattern**: The decorator itself is simple and has no dependencies. Adding Scrutor just for `Decorate()` is overhead we don't need.

**Trade-off acknowledged**: Manual registration means ~17 additional lines in Program.cs instead of 1. But this is actually clearer and more maintainable.

### 3. Training Module Regression Confirmation

**Before (Training module - CreateExerciseCommandHandler.cs):**
```csharp
public sealed class CreateExerciseCommandHandler : ICommandHandler<CreateExerciseCommand, ExerciseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ICommandValidator<CreateExerciseCommand> _validator;

    public CreateExerciseCommandHandler(
        IUnitOfWork unitOfWork,
        IExerciseRepository exerciseRepository,
        ICommandValidator<CreateExerciseCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _exerciseRepository = exerciseRepository;
        _validator = validator;
    }

    public async Task<Result<ExerciseResponse>> HandleAsync(CreateExerciseCommand command, CancellationToken cancellationToken = default)
    {
        // VALIDATION HAPPENS HERE
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
        {
            return Result<ExerciseResponse>.Failure(validationResult.Error);
        }
        // ... business logic
    }
}
```

**After (Training module - CreateExerciseCommandHandler.cs):**
```csharp
public sealed class CreateExerciseCommandHandler : ICommandHandler<CreateExerciseCommand, ExerciseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExerciseRepository _exerciseRepository;
    // NO validator injection

    public CreateExerciseCommandHandler(
        IUnitOfWork unitOfWork,
        IExerciseRepository exerciseRepository)
    {
        _unitOfWork = unitOfWork;
        _exerciseRepository = exerciseRepository;
    }

    public async Task<Result<ExerciseResponse>> HandleAsync(CreateExerciseCommand command, CancellationToken cancellationToken = default)
    {
        // NO validation call - decorator handles it
        var existingExercise = await _exerciseRepository.GetByNameAsync(command.Name, cancellationToken);
        // ... business logic (starts immediately)
    }
}
```

**Program.cs registration:**
```csharp
// Handler (unchanged)
builder.Services.AddScoped<ICommandHandler<CreateExerciseCommand, ExerciseResponse>, CreateExerciseCommandHandler>();

// Validator (already exists)
builder.Services.AddScoped<ICommandValidator<CreateExerciseCommand>, CreateExerciseCommandValidator>();

// NEW: Decorator registration (explicit, one per command)
builder.Services.Decorate<ICommandHandler<CreateExerciseCommand, ExerciseResponse>, ValidatingCommandHandlerDecorator<CreateExerciseCommand, ExerciseResponse>>();
```

**Zero functional regression confirmed:**
- Commands are validated BEFORE reaching the handler (decorator runs first)
- If validation fails, the handler is never called (same as before)
- If validation passes, the handler runs the exact same business logic
- Response type and error message format are identical (`Result<T>.Failure(error)`)

The only difference: validation happens in the decorator's `HandleAsync()` instead of the handler's first lines. Behavior is identical.