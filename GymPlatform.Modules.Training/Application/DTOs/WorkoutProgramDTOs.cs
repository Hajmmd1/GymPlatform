using GymPlatform.Modules.Training.Domain.Enums;

namespace GymPlatform.Modules.Training.Application.DTOs;

public sealed record CreateWorkoutProgramRequest(
    string Name,
    string? Description,
    ExerciseCategory Category,
    DifficultyLevel Difficulty,
    Guid CoachId,
    int DurationWeeks,
    List<string>? Tags);

public sealed record WorkoutProgramResponse(
    Guid Id,
    string Name,
    string? Description,
    ExerciseCategory Category,
    DifficultyLevel Difficulty,
    Guid CoachId,
    int DurationWeeks,
    int ExerciseCount,
    int Version,
    DateTimeOffset CreatedAt,
    bool IsActive,
    bool IsPublished);

public sealed record ProgramExerciseDto(
    Guid ExerciseId,
    int Sets,
    int Reps,
    int Order,
    int? RestSeconds,
    string? Notes);