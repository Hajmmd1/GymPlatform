using GymPlatform.Modules.Training.Domain.Enums;

namespace GymPlatform.Modules.Training.Application.DTOs;

public sealed record RecordBodyMeasurementRequest(
    Guid MemberId,
    MeasurementType Type,
    decimal Value,
    string? Unit,
    DateTimeOffset MeasuredAt,
    string? Notes);

public sealed record BodyMeasurementResponse(
    Guid Id,
    Guid MemberId,
    MeasurementType Type,
    decimal Value,
    string? Unit,
    DateTimeOffset MeasuredAt,
    DateTimeOffset RecordedAt,
    string? Notes);