using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.Modules.Training.Domain.Enums;

namespace GymPlatform.Modules.Training.Application.Commands.RecordBodyMeasurement;

public sealed class RecordBodyMeasurementCommand : ICommand<BodyMeasurementResponse>
{
    public RecordBodyMeasurementCommand(Guid memberId, MeasurementType type, decimal value, string? unit, DateTimeOffset measuredAt, string? notes)
    {
        MemberId = memberId;
        Type = type;
        Value = value;
        Unit = unit;
        MeasuredAt = measuredAt;
        Notes = notes;
    }

    public Guid MemberId { get; }
    public MeasurementType Type { get; }
    public decimal Value { get; }
    public string? Unit { get; }
    public DateTimeOffset MeasuredAt { get; }
    public string? Notes { get; }
}