using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;

namespace GymPlatform.Modules.Training.Application.Commands.LogWorkout;

public sealed class LogWorkoutCommand : ICommand<WorkoutLogResponse>
{
    public LogWorkoutCommand(Guid memberId, Guid programId, Guid? exerciseId, int? setsCompleted, int? repsCompleted, decimal? weightUsed, TimeSpan? duration, string? notes)
    {
        MemberId = memberId;
        ProgramId = programId;
        ExerciseId = exerciseId;
        SetsCompleted = setsCompleted;
        RepsCompleted = repsCompleted;
        WeightUsed = weightUsed;
        Duration = duration;
        Notes = notes;
    }

    public Guid MemberId { get; }
    public Guid ProgramId { get; }
    public Guid? ExerciseId { get; }
    public int? SetsCompleted { get; }
    public int? RepsCompleted { get; }
    public decimal? WeightUsed { get; }
    public TimeSpan? Duration { get; }
    public string? Notes { get; }
}