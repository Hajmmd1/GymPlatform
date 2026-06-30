namespace GymPlatform.Modules.Membership.Application.DTOs;

public sealed record GymResponse(
    Guid Id,
    string Name,
    string? Description,
    DateTimeOffset CreatedAt,
    bool IsActive);
