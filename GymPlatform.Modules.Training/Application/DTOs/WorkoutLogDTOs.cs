namespace GymPlatform.Modules.Training.Application.DTOs;

public sealed record LogWorkoutRequest(
    Guid MemberId,
    Guid ProgramId,
    Guid? ExerciseId,
    int? SetsCompleted,
    int? RepsCompleted,
    decimal? WeightUsed,
    TimeSpan? Duration,
    string? Notes);

public sealed record WorkoutLogResponse(
    Guid Id,
    Guid MemberId,
    Guid ProgramId,
    Guid? ExerciseId,
    int? SetsCompleted,
    int? RepsCompleted,
    decimal? WeightUsed,
    TimeSpan? Duration,
    DateTimeOffset LoggedAt,
    DateTimeOffset CompletedAt,
    string? Notes,
    bool IsCompleted);