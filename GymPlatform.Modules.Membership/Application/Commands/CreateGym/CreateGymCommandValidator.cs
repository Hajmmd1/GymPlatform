using GymPlatform.Modules.Membership.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Application.Commands.CreateGym;

public sealed class CreateGymCommandValidator : ICommandValidator<CreateGymCommand>
{
    public Result Validate(CreateGymCommand command)
    {
        if (command is null)
        {
            return Result.Failure("Command cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Failure("Gym name is required.");
        }

        return Result.Success();
    }
}