using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Commands.CancelBooking;

public class CancelBookingCommandValidator : ICommandValidator<CancelBookingCommand>
{
    public Result Validate(CancelBookingCommand command)
    {
        if (command.BookingId == Guid.Empty)
        {
            return Result.Failure("Booking identifier is required.");
        }

        return Result.Success();
    }
}
