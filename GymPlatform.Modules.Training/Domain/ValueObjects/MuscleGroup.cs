using GymPlatform.Modules.Training.Domain.Exceptions;

namespace GymPlatform.Modules.Training.Domain.ValueObjects;

public sealed class MuscleGroup
{
    private const int MaxLength = 50;

    private MuscleGroup()
    {
    }

    public MuscleGroup(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new TrainingDomainException("Muscle group name is required.");
        }

        var trimmed = value.Trim();

        if (trimmed.Length > MaxLength)
        {
            throw new TrainingDomainException($"Muscle group name cannot exceed {MaxLength} characters.");
        }

        Value = trimmed;
    }

    public string Value { get; private set; } = string.Empty;

    public static readonly MuscleGroup Chest = new("Chest");
    public static readonly MuscleGroup Back = new("Back");
    public static readonly MuscleGroup Shoulders = new("Shoulders");
    public static readonly MuscleGroup Arms = new("Arms");
    public static readonly MuscleGroup Legs = new("Legs");
    public static readonly MuscleGroup Core = new("Core");
    public static readonly MuscleGroup FullBody = new("Full Body");

    public override string ToString()
    {
        return Value;
    }
}