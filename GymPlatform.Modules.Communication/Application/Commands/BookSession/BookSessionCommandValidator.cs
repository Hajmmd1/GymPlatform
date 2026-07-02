using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Commands.BookSession;

public class BookSessionCommandValidator : ICommandValidator<BookSessionCommand>
{
    public Result Validate(BookSessionCommand command)
    {
        if (command.SessionId == Guid.Empty)
        {
            return Result.Failure("Session identifier is required.");
        }

        if (command.MemberId == Guid.Empty)
        {
            return Result.Failure("Member identifier is required.");
        }

        return Result.Success();
    }
}
