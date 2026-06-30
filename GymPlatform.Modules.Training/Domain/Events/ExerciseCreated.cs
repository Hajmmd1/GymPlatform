using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Events;

public sealed class ExerciseCreated : DomainEventBase
{
    public ExerciseCreated(Guid exerciseId, Guid coachId, string name)
    {
        ExerciseId = exerciseId;
        CoachId = coachId;
        Name = name;
    }

    public Guid ExerciseId { get; }
    public Guid CoachId { get; }
    public string Name { get; }
}