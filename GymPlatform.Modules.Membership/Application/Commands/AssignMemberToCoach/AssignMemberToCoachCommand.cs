using GymPlatform.Modules.Membership.Application.DTOs;
using GymPlatform.Modules.Membership.Application.Interfaces;

namespace GymPlatform.Modules.Membership.Application.Commands.AssignMemberToCoach;

public sealed record AssignMemberToCoachCommand(Guid MemberId, Guid CoachId) : ICommand<MemberCoachAssignmentResponse>;