using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Commands.CancelSession;

public class CancelSessionCommandValidator : ICommandValidator<CancelSessionCommand>
{
    public Result Validate(CancelSessionCommand command)
    {
        if (command.SessionId == Guid.Empty)
        {
            return Result.Failure("Session identifier is required.");
        }

        return Result.Success();
    }
}
