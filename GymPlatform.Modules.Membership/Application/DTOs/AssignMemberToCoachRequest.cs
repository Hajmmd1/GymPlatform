using GymPlatform.Modules.Membership.Domain.Enums;

namespace GymPlatform.Modules.Membership.Application.DTOs;

public sealed record AssignMemberToCoachRequest(Guid MemberId, Guid CoachId);
