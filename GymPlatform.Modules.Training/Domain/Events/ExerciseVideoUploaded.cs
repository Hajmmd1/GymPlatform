using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Events;

public sealed class ExerciseVideoUploaded : DomainEventBase
{
    public ExerciseVideoUploaded(Guid videoId, Guid exerciseId, Guid coachId)
    {
        VideoId = videoId;
        ExerciseId = exerciseId;
        CoachId = coachId;
    }

    public Guid VideoId { get; }
    public Guid ExerciseId { get; }
    public Guid CoachId { get; }
}