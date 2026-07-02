using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Application.Interfaces;

namespace GymPlatform.Modules.Communication.Application.Commands.SetCoachAvailability;

public record SetCoachAvailabilityCommand(Guid CoachId, Guid GymId, DateTimeOffset StartTime, DateTimeOffset EndTime, bool IsAvailable) : ICommand<CoachAvailabilityResponse>;
