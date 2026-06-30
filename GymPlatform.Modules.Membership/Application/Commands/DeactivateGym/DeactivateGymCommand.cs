using GymPlatform.Modules.Membership.Application.DTOs;
using GymPlatform.Modules.Membership.Application.Interfaces;

namespace GymPlatform.Modules.Membership.Application.Commands.DeactivateGym;

public sealed record DeactivateGymCommand(Guid GymId) : ICommand<GymResponse>;