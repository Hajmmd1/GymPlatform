using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.Modules.Training.Domain.Enums;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.RecordBodyMeasurement;

public sealed class RecordBodyMeasurementCommandValidator : ICommandValidator<RecordBodyMeasurementCommand>
{
    public Result Validate(RecordBodyMeasurementCommand command)
    {
        if (command == null)
        {
            return Result.Failure("Command cannot be null.");
        }

        if (command.MemberId == Guid.Empty)
        {
            return Result.Failure("Member identifier is required.");
        }

        if (!Enum.IsDefined(command.Type))
        {
            return Result.Failure("Invalid measurement type.");
        }

        if (command.Value <= 0)
        {
            return Result.Failure("Measurement value must be greater than zero.");
        }

        return Result.Success();
    }
}