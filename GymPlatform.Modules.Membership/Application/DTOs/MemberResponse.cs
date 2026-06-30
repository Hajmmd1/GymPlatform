namespace GymPlatform.Modules.Membership.Application.DTOs;

public sealed record MemberResponse(
    Guid Id,
    Guid GymId,
    string FullName,
    string Email,
    string? Phone,
    string Status,
    DateTimeOffset CreatedAt,
    Guid? AssignedCoachId);
