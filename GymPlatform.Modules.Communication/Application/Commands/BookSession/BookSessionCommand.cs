using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Application.Interfaces;

namespace GymPlatform.Modules.Communication.Application.Commands.BookSession;

public record BookSessionCommand(Guid SessionId, Guid MemberId) : ICommand<BookingResponse>;
