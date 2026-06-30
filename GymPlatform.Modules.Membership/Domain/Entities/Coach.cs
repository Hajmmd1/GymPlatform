using GymPlatform.Modules.Membership.Domain.Exceptions;
using GymPlatform.Modules.Membership.Domain.ValueObjects;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Domain.Entities;

public sealed class Coach : BaseEntity
{
    private const int MaxFullNameLength = 200;
    private const int MaxSpecialtyLength = 100;

    private Coach()
    {
    }

    public Coach(Guid gymId, string fullName, Email email, bool gymIsActive, string? specialty = null, bool isActive = true)
    {
        EnsureGymId(gymId);
        EnsureGymIsActive(gymIsActive);
        SetFullName(fullName);
        SetEmail(email);
        SetSpecialty(specialty);
        IsActive = isActive;
    }

    public Guid GymId { get; private set; }

    public string FullName { get; private set; } = string.Empty;

    public Email Email { get; private set; } = null!;

    public string? Specialty { get; private set; }

    public bool IsActive { get; private set; }

    public void AssignToGym(Guid gymId, bool gymIsActive)
    {
        EnsureGymId(gymId);
        EnsureGymIsActive(gymIsActive);

        GymId = gymId;
    }

    public void SetFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new MembershipDomainException("Coach full name is required.");
        }

        var trimmed = fullName.Trim();

        if (trimmed.Length > MaxFullNameLength)
        {
            throw new MembershipDomainException($"Coach full name cannot exceed {MaxFullNameLength} characters.");
        }

        FullName = trimmed;
    }

    public void SetEmail(Email email)
    {
        Email = email ?? throw new MembershipDomainException("Coach email is required.");
    }

    public void SetSpecialty(string? specialty)
    {
        Specialty = string.IsNullOrWhiteSpace(specialty)
            ? null
            : specialty.Trim().Length <= MaxSpecialtyLength
                ? specialty.Trim()
                : throw new MembershipDomainException($"Coach specialty cannot exceed {MaxSpecialtyLength} characters.");
    }

    public void Activate(bool gymIsActive)
    {
        EnsureGymId(GymId);
        EnsureGymIsActive(gymIsActive);

        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void EnsureActive()
    {
        if (!IsActive)
        {
            throw new MembershipDomainException("Coach must be active.");
        }
    }

    private static void EnsureGymId(Guid gymId)
    {
        if (gymId == Guid.Empty)
        {
            throw new MembershipDomainException("Gym identifier is required.");
        }
    }

    private static void EnsureGymIsActive(bool gymIsActive)
    {
        if (!gymIsActive)
        {
            throw new MembershipDomainException("Coach cannot belong to an inactive gym.");
        }
    }
}
