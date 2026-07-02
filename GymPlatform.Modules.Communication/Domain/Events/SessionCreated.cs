using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Domain.Events;

public sealed class SessionCreated : DomainEventBase
{
    public SessionCreated(Guid sessionId, Guid gymId, Guid coachId, string sessionName, DateTimeOffset startTime)
    {
        SessionId = sessionId;
        GymId = gymId;
        CoachId = coachId;
        SessionName = sessionName;
        StartTime = startTime;
    }

    public Guid SessionId { get; }
    public Guid GymId { get; }
    public Guid CoachId { get; }
    public string SessionName { get; }
    public DateTimeOffset StartTime { get; }
}
