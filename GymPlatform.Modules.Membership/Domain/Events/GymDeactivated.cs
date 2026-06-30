using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Domain.Events;

public sealed class GymDeactivated : DomainEventBase
{
    public GymDeactivated(Guid gymId)
    {
        GymId = gymId;
    }

    public Guid GymId { get; }
}
