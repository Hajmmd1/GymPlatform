using GymPlatform.Modules.Membership.Domain.Enums;
using GymPlatform.Modules.Membership.Domain.Events;
using GymPlatform.Modules.Membership.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Domain.Entities;

public sealed class Gym : BaseEntity
{
    private const int MaxNameLength = 200;
    private const int MaxDescriptionLength = 1000;

    private Gym()
    {
    }

    public Gym(string name, string? description = null)
    {
        SetName(name);
        SetDescription(description);
        CreatedAt = DateTimeOffset.UtcNow;
        IsActive = true;

        AddDomainEvent(new GymCreated(Id, Name));
    }

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public bool IsActive { get; private set; }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new MembershipDomainException("Gym name is required.");
        }

        var trimmed = name.Trim();

        if (trimmed.Length > MaxNameLength)
        {
            throw new MembershipDomainException($"Gym name cannot exceed {MaxNameLength} characters.");
        }

        Name = trimmed;
    }

    public void SetDescription(string? description)
    {
        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim().Length <= MaxDescriptionLength
                ? description.Trim()
                : throw new MembershipDomainException($"Gym description cannot exceed {MaxDescriptionLength} characters.");
    }

    public void Activate()
    {
        if (IsActive)
        {
            throw new MembershipDomainException("Gym is already active.");
        }

        IsActive = true;
        AddDomainEvent(new GymActivated(Id));
    }

    public void Deactivate()
    {
        if (!IsActive)
        {
            throw new MembershipDomainException("Gym is already inactive.");
        }

        IsActive = false;
        AddDomainEvent(new GymDeactivated(Id));
    }

    public void EnsureActive()
    {
        if (!IsActive)
        {
            throw new MembershipDomainException("Gym must be active.");
        }
    }
}
