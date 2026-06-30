namespace GymPlatform.Modules.Membership.Application.DTOs;

public sealed record MemberCoachAssignmentResponse(Guid MemberId, Guid CoachId, Guid? AssignedCoachId);
