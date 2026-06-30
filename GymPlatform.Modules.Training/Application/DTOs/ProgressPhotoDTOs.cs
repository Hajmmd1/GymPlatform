namespace GymPlatform.Modules.Training.Application.DTOs;

public sealed record UploadProgressPhotoRequest(
    Guid MemberId,
    string PhotoUrl,
    DateTimeOffset CapturedAt,
    string? ThumbnailUrl,
    string? Notes,
    bool IsPrivate = true);

public sealed record ProgressPhotoResponse(
    Guid Id,
    Guid MemberId,
    string PhotoUrl,
    DateTimeOffset CapturedAt,
    DateTimeOffset UploadedAt,
    string? ThumbnailUrl,
    string? Notes,
    bool IsPrivate,
    bool IsApproved);