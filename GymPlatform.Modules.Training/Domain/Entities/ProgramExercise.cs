using GymPlatform.Modules.Training.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Entities;

public sealed class ProgramExercise
{
    internal ProgramExercise(
        Guid exerciseId,
        int sets,
        int reps,
        int order)
    {
        SetExerciseId(exerciseId);
        SetSetsReps(sets, reps);
        Order = order;
    }

    public Guid ExerciseId { get; private set; }

    public int Sets { get; private set; }

    public int Reps { get; private set; }

    public int Order { get; private set; }

    public int? RestSeconds { get; private set; }

    public string? Notes { get; private set; }

    public void SetExerciseId(Guid exerciseId)
    {
        if (exerciseId == Guid.Empty)
        {
            throw new TrainingDomainException("Exercise identifier is required.");
        }

        ExerciseId = exerciseId;
    }

    public void SetRestSeconds(int? restSeconds)
    {
        RestSeconds = restSeconds >= 0 ? restSeconds : null;
    }

    public void SetNotes(string? notes)
    {
        Notes = notes?.Trim();
    }

    public void SetSetsReps(int sets, int reps)
    {
        if (sets < 1)
        {
            throw new TrainingDomainException("Sets must be at least 1.");
        }

        if (reps < 1)
        {
            throw new TrainingDomainException("Reps must be at least 1.");
        }

        Sets = sets;
        Reps = reps;
    }
}