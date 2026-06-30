namespace GymPlatform.Modules.Training.Application.DTOs;

public sealed record UploadExerciseVideoRequest(
    Guid ExerciseId,
    string Title,
    string VideoUrl,
    string? Description,
    string? ThumbnailUrl,
    int? DurationSeconds,
    Guid CoachId);

public sealed record ExerciseVideoResponse(
    Guid Id,
    Guid ExerciseId,
    string Title,
    string? Description,
    string VideoUrl,
    string? ThumbnailUrl,
    int? DurationSeconds,
    Guid CoachId,
    DateTimeOffset CreatedAt,
    bool IsApproved,
    int ViewCount);