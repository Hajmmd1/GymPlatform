using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Domain.Events;

public sealed class GymCreated : DomainEventBase
{
    public GymCreated(Guid gymId, string name)
    {
        GymId = gymId;
        Name = name;
    }

    public Guid GymId { get; }

    public string Name { get; }
}
