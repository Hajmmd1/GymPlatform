using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Domain.Events;

public sealed class SessionCancelled : DomainEventBase
{
    public SessionCancelled(Guid sessionId, Guid gymId)
    {
        SessionId = sessionId;
        GymId = gymId;
    }

    public Guid SessionId { get; }
    public Guid GymId { get; }
}
