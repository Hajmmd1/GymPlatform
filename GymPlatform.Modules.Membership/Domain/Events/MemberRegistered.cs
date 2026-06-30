using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Domain.Events;

public sealed class MemberRegistered : DomainEventBase
{
    public MemberRegistered(Guid memberId, Guid gymId, string email)
    {
        MemberId = memberId;
        GymId = gymId;
        Email = email;
    }

    public Guid MemberId { get; }

    public Guid GymId { get; }

    public string Email { get; }
}
