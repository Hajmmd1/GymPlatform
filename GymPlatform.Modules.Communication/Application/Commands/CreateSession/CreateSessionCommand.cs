using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.Modules.Communication.Domain.Enums;

namespace GymPlatform.Modules.Communication.Application.Commands.CreateSession;

public record CreateSessionCommand(Guid GymId, Guid CoachId, Guid? RoomId, string Name, SessionType SessionType, DateTimeOffset StartTime, DateTimeOffset EndTime, int MaxCapacity) : ICommand<SessionResponse>;
