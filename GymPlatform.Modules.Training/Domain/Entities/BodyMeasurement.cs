using GymPlatform.Modules.Training.Domain.Enums;
using GymPlatform.Modules.Training.Domain.Events;
using GymPlatform.Modules.Training.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Entities;

public sealed class BodyMeasurement : BaseEntity
{
    private BodyMeasurement()
    {
    }

    public BodyMeasurement(Guid memberId, MeasurementType type, decimal value, DateTimeOffset measuredAt)
    {
        SetMemberId(memberId);
        SetMeasurementType(type);
        SetValue(value);
        SetMeasuredAt(measuredAt);

        RecordedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new BodyMeasurementRecorded(Id, memberId, type.ToString(), value));
    }

    public Guid MemberId { get; private set; }

    public MeasurementType Type { get; private set; }

    public decimal Value { get; private set; }

    public string? Unit { get; private set; }

    public DateTimeOffset MeasuredAt { get; private set; }

    public DateTimeOffset RecordedAt { get; private set; }

    public string? Notes { get; private set; }

    public void SetMemberId(Guid memberId)
    {
        if (memberId == Guid.Empty)
        {
            throw new TrainingDomainException("Member identifier is required.");
        }

        MemberId = memberId;
    }

    public void SetMeasurementType(MeasurementType type)
    {
        Type = type;
    }

    public void SetValue(decimal value)
    {
        if (value <= 0)
        {
            throw new TrainingDomainException("Measurement value must be greater than zero.");
        }

        Value = value;
    }

    public void SetUnit(string? unit)
    {
        Unit = string.IsNullOrWhiteSpace(unit) ? null : unit.Trim().ToLowerInvariant();
    }

    public void SetMeasuredAt(DateTimeOffset measuredAt)
    {
        MeasuredAt = measuredAt;
    }

    public void SetNotes(string? notes)
    {
        Notes = notes?.Trim();
    }

    public bool IsTrendingUp(IEnumerable<decimal> previousValues)
    {
        var values = previousValues.ToList();
        if (values.Count < 2)
        {
            return false;
        }

        return Value > values.Average();
    }

    public bool IsTrendingDown(IEnumerable<decimal> previousValues)
    {
        var values = previousValues.ToList();
        if (values.Count < 2)
        {
            return false;
        }

        return Value < values.Average();
    }
}