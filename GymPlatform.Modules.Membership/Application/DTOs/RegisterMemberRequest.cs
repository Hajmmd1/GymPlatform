using GymPlatform.Modules.Membership.Domain.Enums;

namespace GymPlatform.Modules.Membership.Application.DTOs;

public sealed record RegisterMemberRequest(
    Guid GymId,
    string FullName,
    string Email,
    string? Phone,
    MemberStatus Status);
