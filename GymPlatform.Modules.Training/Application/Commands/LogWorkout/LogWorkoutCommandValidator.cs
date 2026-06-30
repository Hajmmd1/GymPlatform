using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.LogWorkout;

public sealed class LogWorkoutCommandValidator : ICommandValidator<LogWorkoutCommand>
{
    public Result Validate(LogWorkoutCommand command)
    {
        if (command == null)
        {
            return Result.Failure("Command cannot be null.");
        }

        if (command.MemberId == Guid.Empty)
        {
            return Result.Failure("Member identifier is required.");
        }

        if (command.ProgramId == Guid.Empty)
        {
            return Result.Failure("Program identifier is required.");
        }

        return Result.Success();
    }
}