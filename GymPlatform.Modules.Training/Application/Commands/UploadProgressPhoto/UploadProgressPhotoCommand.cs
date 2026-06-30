using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;

namespace GymPlatform.Modules.Training.Application.Commands.UploadProgressPhoto;

public sealed class UploadProgressPhotoCommand : ICommand<ProgressPhotoResponse>
{
    public UploadProgressPhotoCommand(Guid memberId, string photoUrl, DateTimeOffset capturedAt, string? thumbnailUrl, string? notes, bool isPrivate)
    {
        MemberId = memberId;
        PhotoUrl = photoUrl;
        CapturedAt = capturedAt;
        ThumbnailUrl = thumbnailUrl;
        Notes = notes;
        IsPrivate = isPrivate;
    }

    public Guid MemberId { get; }
    public string PhotoUrl { get; }
    public DateTimeOffset CapturedAt { get; }
    public string? ThumbnailUrl { get; }
    public string? Notes { get; }
    public bool IsPrivate { get; }
}