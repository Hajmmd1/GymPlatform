using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Application.Interfaces;

namespace GymPlatform.Modules.Communication.Application.Commands.CancelBooking;

public record CancelBookingCommand(Guid BookingId) : ICommand<BookingResponse>;
