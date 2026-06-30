using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;

namespace GymPlatform.Modules.Training.Application.Commands.UploadExerciseVideo;

public sealed class UploadExerciseVideoCommand : ICommand<ExerciseVideoResponse>
{
    public UploadExerciseVideoCommand(Guid exerciseId, string title, string videoUrl, string? description, string? thumbnailUrl, int? durationSeconds, Guid coachId)
    {
        ExerciseId = exerciseId;
        Title = title;
        VideoUrl = videoUrl;
        Description = description;
        ThumbnailUrl = thumbnailUrl;
        DurationSeconds = durationSeconds;
        CoachId = coachId;
    }

    public Guid ExerciseId { get; }
    public string Title { get; }
    public string VideoUrl { get; }
    public string? Description { get; }
    public string? ThumbnailUrl { get; }
    public int? DurationSeconds { get; }
    public Guid CoachId { get; }
}