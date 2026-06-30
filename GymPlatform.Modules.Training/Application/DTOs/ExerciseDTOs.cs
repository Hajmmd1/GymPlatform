using GymPlatform.Modules.Training.Domain.Enums;

namespace GymPlatform.Modules.Training.Application.DTOs;

public sealed record CreateExerciseRequest(
    string Name,
    string? Description,
    ExerciseCategory Category,
    DifficultyLevel Difficulty,
    Guid CoachId,
    string? Equipment);

public sealed record ExerciseResponse(
    Guid Id,
    string Name,
    string? Description,
    ExerciseCategory Category,
    DifficultyLevel Difficulty,
    string? EquipmentName,
    Guid CoachId,
    DateTimeOffset CreatedAt,
    bool IsActive);