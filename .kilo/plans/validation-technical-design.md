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