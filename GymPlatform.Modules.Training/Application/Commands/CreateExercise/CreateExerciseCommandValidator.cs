using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.Modules.Training.Domain.Enums;
using GymPlatform.Modules.Training.Domain.ValueObjects;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.CreateExercise;

public sealed class CreateExerciseCommandValidator : ICommandValidator<CreateExerciseCommand>
{
    public Result Validate(CreateExerciseCommand command)
    {
        if (command == null)
        {
            return Result.Failure("Command cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Failure("Exercise name is required.");
        }

        if (!Enum.IsDefined(command.Category))
        {
            return Result.Failure("Invalid exercise category.");
        }

        if (!Enum.IsDefined(command.Difficulty))
        {
            return Result.Failure("Invalid difficulty level.");
        }

        if (command.CoachId == Guid.Empty)
        {
            return Result.Failure("Coach identifier is required.");
        }

        return Result.Success();
    }
}