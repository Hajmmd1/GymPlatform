using GymPlatform.Modules.Membership.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Application.Commands.AssignMemberToCoach;

public sealed class AssignMemberToCoachCommandValidator : ICommandValidator<AssignMemberToCoachCommand>
{
    public Result Validate(AssignMemberToCoachCommand command)
    {
        if (command is null)
        {
            return Result.Failure("Command cannot be null.");
        }

        if (command.MemberId == Guid.Empty)
        {
            return Result.Failure("Member identifier is required.");
        }

        if (command.CoachId == Guid.Empty)
        {
            return Result.Failure("Coach identifier is required.");
        }

        return Result.Success();
    }
}