namespace GymPlatform.Modules.Training.Application.DTOs;

public sealed record UpdateCoachProfileRequest(
    Guid CoachId,
    string? Bio,
    string? ProfilePhotoUrl,
    List<string>? Specialties);

public sealed record CoachProfileResponse(
    Guid Id,
    Guid CoachId,
    string? Bio,
    string? ProfilePhotoUrl,
    IReadOnlyCollection<string> Specialties,
    decimal? Rating,
    int RatingCount,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    bool IsActive);