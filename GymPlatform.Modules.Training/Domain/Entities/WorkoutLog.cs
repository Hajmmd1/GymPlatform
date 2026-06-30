using GymPlatform.Modules.Training.Domain.Events;
using GymPlatform.Modules.Training.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Entities;

public sealed class WorkoutLog : BaseEntity
{
    private const int MaxNotesLength = 500;

    private WorkoutLog()
    {
    }

    public WorkoutLog(Guid memberId, Guid programId)
    {
        SetMemberId(memberId);
        SetProgramId(programId);

        LoggedAt = DateTimeOffset.UtcNow;
        CompletedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new WorkoutLogCreated(Id, memberId, programId));
    }

    public Guid MemberId { get; private set; }

    public Guid ProgramId { get; private set; }

    public Guid? ExerciseId { get; private set; }

    public int? SetsCompleted { get; private set; }

    public int? RepsCompleted { get; private set; }

    public decimal? WeightUsed { get; private set; }

    public TimeSpan? Duration { get; private set; }

    public DateTimeOffset LoggedAt { get; private set; }

    public DateTimeOffset CompletedAt { get; private set; }

    public string? Notes { get; private set; }

    public bool IsCompleted { get; private set; }

    public void SetMemberId(Guid memberId)
    {
        if (memberId == Guid.Empty)
        {
            throw new TrainingDomainException("Member identifier is required.");
        }

        MemberId = memberId;
    }

    public void SetProgramId(Guid programId)
    {
        if (programId == Guid.Empty)
        {
            throw new TrainingDomainException("Program identifier is required.");
        }

        ProgramId = programId;
    }

    public void SetExerciseId(Guid exerciseId)
    {
        ExerciseId = exerciseId;
    }

    public void Complete(int setsCompleted, int repsCompleted, decimal? weightUsed = null, TimeSpan? duration = null)
    {
        SetsCompleted = setsCompleted >= 1 ? setsCompleted : null;
        RepsCompleted = repsCompleted >= 1 ? repsCompleted : null;
        WeightUsed = weightUsed > 0 ? weightUsed : null;
        Duration = duration;
        IsCompleted = true;
    }

    public void SetNotes(string? notes)
    {
        if (!string.IsNullOrWhiteSpace(notes) && notes.Trim().Length > MaxNotesLength)
        {
            throw new TrainingDomainException($"Notes cannot exceed {MaxNotesLength} characters.");
        }

        Notes = notes?.Trim();
    }
}