using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Commands.CreateSession;

public class CreateSessionCommandValidator : ICommandValidator<CreateSessionCommand>
{
    public Result Validate(CreateSessionCommand command)
    {
        if (command.GymId == Guid.Empty)
        {
            return Result.Failure("Gym identifier is required.");
        }

        if (command.CoachId == Guid.Empty)
        {
            return Result.Failure("Coach identifier is required.");
        }

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Failure("Session name is required.");
        }

        if (command.EndTime <= command.StartTime)
        {
            return Result.Failure("End time must be after start time.");
        }

        if (command.MaxCapacity < 1)
        {
            return Result.Failure("Maximum capacity must be at least 1.");
        }

        return Result.Success();
    }
}
