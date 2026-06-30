using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.Modules.Training.Domain.Enums;

namespace GymPlatform.Modules.Training.Application.Commands.CreateWorkoutProgram;

public sealed class CreateWorkoutProgramCommand : ICommand<WorkoutProgramResponse>
{
    public CreateWorkoutProgramCommand(string name, string? description, ExerciseCategory category, DifficultyLevel difficulty, Guid coachId, int durationWeeks, List<string>? tags)
    {
        Name = name;
        Description = description;
        Category = category;
        Difficulty = difficulty;
        CoachId = coachId;
        DurationWeeks = durationWeeks;
        Tags = tags;
    }

    public string Name { get; }
    public string? Description { get; }
    public ExerciseCategory Category { get; }
    public DifficultyLevel Difficulty { get; }
    public Guid CoachId { get; }
    public int DurationWeeks { get; }
    public List<string>? Tags { get; }
}