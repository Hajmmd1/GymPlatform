using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Commands.SetCoachAvailability;

public class SetCoachAvailabilityCommandValidator : ICommandValidator<SetCoachAvailabilityCommand>
{
    public Result Validate(SetCoachAvailabilityCommand command)
    {
        if (command.CoachId == Guid.Empty)
        {
            return Result.Failure("Coach identifier is required.");
        }

        if (command.GymId == Guid.Empty)
        {
            return Result.Failure("Gym identifier is required.");
        }

        if (command.EndTime <= command.StartTime)
        {
            return Result.Failure("End time must be after start time.");
        }

        return Result.Success();
    }
}
