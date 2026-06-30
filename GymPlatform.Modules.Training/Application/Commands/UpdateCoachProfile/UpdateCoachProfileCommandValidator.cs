using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.UpdateCoachProfile;

public sealed class UpdateCoachProfileCommandValidator : ICommandValidator<UpdateCoachProfileCommand>
{
    public Result Validate(UpdateCoachProfileCommand command)
    {
        if (command == null)
        {
            return Result.Failure("Command cannot be null.");
        }

        if (command.CoachId == Guid.Empty)
        {
            return Result.Failure("Coach identifier is required.");
        }

        return Result.Success();
    }
}