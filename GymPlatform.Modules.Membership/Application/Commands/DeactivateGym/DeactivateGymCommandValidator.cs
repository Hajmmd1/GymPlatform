using GymPlatform.Modules.Membership.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Application.Commands.DeactivateGym;

public sealed class DeactivateGymCommandValidator : ICommandValidator<DeactivateGymCommand>
{
    public Result Validate(DeactivateGymCommand command)
    {
        if (command is null)
        {
            return Result.Failure("Command cannot be null.");
        }

        if (command.GymId == Guid.Empty)
        {
            return Result.Failure("Gym identifier is required.");
        }

        return Result.Success();
    }
}