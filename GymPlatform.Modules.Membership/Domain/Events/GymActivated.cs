using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Domain.Events;

public sealed class GymActivated : DomainEventBase
{
    public GymActivated(Guid gymId)
    {
        GymId = gymId;
    }

    public Guid GymId { get; }
}
