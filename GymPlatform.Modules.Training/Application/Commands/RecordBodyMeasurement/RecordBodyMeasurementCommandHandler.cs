using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Repositories;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.RecordBodyMeasurement;

public sealed class RecordBodyMeasurementCommandHandler : ICommandHandler<RecordBodyMeasurementCommand, BodyMeasurementResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBodyMeasurementRepository _measurementRepository;
    private readonly ICommandValidator<RecordBodyMeasurementCommand> _validator;

    public RecordBodyMeasurementCommandHandler(
        IUnitOfWork unitOfWork,
        IBodyMeasurementRepository measurementRepository,
        ICommandValidator<RecordBodyMeasurementCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _measurementRepository = measurementRepository;
        _validator = validator;
    }

    public async Task<Result<BodyMeasurementResponse>> HandleAsync(RecordBodyMeasurementCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
        {
            return Result<BodyMeasurementResponse>.Failure(validationResult.Error);
        }

        var measurement = new BodyMeasurement(command.MemberId, command.Type, command.Value, command.MeasuredAt);
        measurement.SetUnit(command.Unit);
        measurement.SetNotes(command.Notes);

        await _measurementRepository.AddAsync(measurement, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<BodyMeasurementResponse>.Success(new BodyMeasurementResponse(
            measurement.Id,
            measurement.MemberId,
            measurement.Type,
            measurement.Value,
            measurement.Unit,
            measurement.MeasuredAt,
            measurement.RecordedAt,
            measurement.Notes));
    }
}