using GymPlatform.Modules.Membership.Domain.Enums;
using GymPlatform.Modules.Membership.Domain.Events;
using GymPlatform.Modules.Membership.Domain.Exceptions;
using GymPlatform.Modules.Membership.Domain.ValueObjects;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Domain.Entities;

public sealed class Member : BaseEntity
{
    private const int MaxFullNameLength = 200;

    private Member()
    {
    }

    public Member(Guid gymId, string fullName, Email email, bool gymIsActive, Phone? phone = null, MemberStatus status = MemberStatus.Active)
    {
        EnsureGymId(gymId);
        EnsureGymIsActive(gymIsActive);
        SetFullName(fullName);
        SetEmail(email);
        SetPhone(phone);
        SetStatus(status);

        CreatedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new MemberRegistered(Id, GymId, Email.Value));
    }

    public Guid GymId { get; private set; }

    public string FullName { get; private set; } = string.Empty;

    public Email Email { get; private set; } = null!;

    public Phone? Phone { get; private set; }

    public MemberStatus Status { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public Guid? AssignedCoachId { get; private set; }

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
            throw new MembershipDomainException("Member full name is required.");
        }

        var trimmed = fullName.Trim();

        if (trimmed.Length > MaxFullNameLength)
        {
            throw new MembershipDomainException($"Member full name cannot exceed {MaxFullNameLength} characters.");
        }

        FullName = trimmed;
    }

    public void SetEmail(Email email)
    {
        Email = email ?? throw new MembershipDomainException("Member email is required.");
    }

    public void SetPhone(Phone? phone)
    {
        Phone = phone;
    }

    public void SetStatus(MemberStatus status)
    {
        if (!IsValidStatus(status))
        {
            throw new MembershipDomainException("Member status is invalid.");
        }

        Status = status;
    }

    public void Activate()
    {
        SetStatus(MemberStatus.Active);
    }

    public void Suspend()
    {
        SetStatus(MemberStatus.Suspended);
    }

    public void Cancel()
    {
        SetStatus(MemberStatus.Cancelled);
    }

    public void AssignToCoach(Guid coachId)
    {
        if (coachId == Guid.Empty)
        {
            throw new MembershipDomainException("Coach identifier is required.");
        }

        AssignedCoachId = coachId;
        AddDomainEvent(new CoachAssigned(Id, coachId));
    }

    public void EnsureCanBelongToGym(bool gymIsActive)
    {
        EnsureGymIsActive(gymIsActive);
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
            throw new MembershipDomainException("Member cannot belong to an inactive gym.");
        }
    }

    private static bool IsValidStatus(MemberStatus status)
    {
        return status is MemberStatus.Active or MemberStatus.Suspended or MemberStatus.Cancelled;
    }
}
