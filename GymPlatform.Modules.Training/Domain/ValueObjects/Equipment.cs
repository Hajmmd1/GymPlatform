using GymPlatform.Modules.Training.Domain.Exceptions;

namespace GymPlatform.Modules.Training.Domain.ValueObjects;

public sealed class Equipment
{
    private const int MaxLength = 100;

    private Equipment()
    {
    }

    public Equipment(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new TrainingDomainException("Equipment name is required.");
        }

        var trimmed = value.Trim();

        if (trimmed.Length > MaxLength)
        {
            throw new TrainingDomainException($"Equipment name cannot exceed {MaxLength} characters.");
        }

        Value = trimmed;
    }

    public string Value { get; private set; } = string.Empty;

    public override string ToString()
    {
        return Value;
    }
}