using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Domain.Events;

public sealed class CoachAssigned : DomainEventBase
{
    public CoachAssigned(Guid memberId, Guid coachId)
    {
        MemberId = memberId;
        CoachId = coachId;
    }

    public Guid MemberId { get; }

    public Guid CoachId { get; }
}
