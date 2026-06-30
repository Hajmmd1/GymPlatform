using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.Modules.Training.Domain.Enums;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.CreateWorkoutProgram;

public sealed class CreateWorkoutProgramCommandValidator : ICommandValidator<CreateWorkoutProgramCommand>
{
    public Result Validate(CreateWorkoutProgramCommand command)
    {
        if (command == null)
        {
            return Result.Failure("Command cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Failure("Workout program name is required.");
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

        if (command.DurationWeeks < 1 || command.DurationWeeks > 52)
        {
            return Result.Failure("Duration must be between 1 and 52 weeks.");
        }

        return Result.Success();
    }
}