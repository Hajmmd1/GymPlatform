using GymPlatform.Modules.Membership.Application.DTOs;
using GymPlatform.Modules.Membership.Application.Interfaces;

namespace GymPlatform.Modules.Membership.Application.Commands.CreateGym;

public sealed record CreateGymCommand(string Name, string? Description) : ICommand<GymResponse>;