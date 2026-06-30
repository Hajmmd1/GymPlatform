using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Events;

public sealed class WorkoutLogCreated : DomainEventBase
{
    public WorkoutLogCreated(Guid logId, Guid memberId, Guid programId)
    {
        LogId = logId;
        MemberId = memberId;
        ProgramId = programId;
    }

    public Guid LogId { get; }
    public Guid MemberId { get; }
    public Guid ProgramId { get; }
}