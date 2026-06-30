using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Events;

public sealed class WorkoutProgramCreated : DomainEventBase
{
    public WorkoutProgramCreated(Guid programId, Guid coachId, string name)
    {
        ProgramId = programId;
        CoachId = coachId;
        Name = name;
    }

    public Guid ProgramId { get; }
    public Guid CoachId { get; }
    public string Name { get; }
}