using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Events;

public sealed class BodyMeasurementRecorded : DomainEventBase
{
    public BodyMeasurementRecorded(Guid measurementId, Guid memberId, string type, decimal value)
    {
        MeasurementId = measurementId;
        MemberId = memberId;
        Type = type;
        Value = value;
    }

    public Guid MeasurementId { get; }
    public Guid MemberId { get; }
    public string Type { get; }
    public decimal Value { get; }
}