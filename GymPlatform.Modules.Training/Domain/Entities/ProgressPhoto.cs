using GymPlatform.Modules.Training.Domain.Events;
using GymPlatform.Modules.Training.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Entities;

public sealed class ProgressPhoto : BaseEntity
{
    private const int MaxNotesLength = 500;

    private ProgressPhoto()
    {
    }

    public ProgressPhoto(Guid memberId, string photoUrl, DateTimeOffset capturedAt)
    {
        SetMemberId(memberId);
        SetPhotoUrl(photoUrl);
        SetCapturedAt(capturedAt);

        UploadedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new ProgressPhotoUploaded(Id, memberId));
    }

    public Guid MemberId { get; private set; }

    public string PhotoUrl { get; private set; } = string.Empty;

    public DateTimeOffset CapturedAt { get; private set; }

    public DateTimeOffset UploadedAt { get; private set; }

    public string? ThumbnailUrl { get; private set; }

    public string? Notes { get; private set; }

    public bool IsPrivate { get; private set; } = true;

    public bool IsApproved { get; private set; }

    public void SetMemberId(Guid memberId)
    {
        if (memberId == Guid.Empty)
        {
            throw new TrainingDomainException("Member identifier is required.");
        }

        MemberId = memberId;
    }

    public void SetPhotoUrl(string photoUrl)
    {
        if (string.IsNullOrWhiteSpace(photoUrl))
        {
            throw new TrainingDomainException("Photo URL is required.");
        }

        PhotoUrl = photoUrl.Trim();
    }

    public void SetCapturedAt(DateTimeOffset capturedAt)
    {
        CapturedAt = capturedAt;
    }

    public void SetThumbnailUrl(string? thumbnailUrl)
    {
        ThumbnailUrl = thumbnailUrl?.Trim();
    }

    public void SetNotes(string? notes)
    {
        if (!string.IsNullOrWhiteSpace(notes) && notes.Trim().Length > MaxNotesLength)
        {
            throw new TrainingDomainException($"Notes cannot exceed {MaxNotesLength} characters.");
        }

        Notes = notes?.Trim();
    }

    public void SetPrivacy(bool isPrivate)
    {
        IsPrivate = isPrivate;
    }

    public void Approve()
    {
        IsApproved = true;
    }

    public void Reject()
    {
        IsApproved = false;
    }
}