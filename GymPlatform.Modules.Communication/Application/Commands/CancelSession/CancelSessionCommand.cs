using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Application.Interfaces;

namespace GymPlatform.Modules.Communication.Application.Commands.CancelSession;

public record CancelSessionCommand(Guid SessionId) : ICommand<SessionResponse>;
