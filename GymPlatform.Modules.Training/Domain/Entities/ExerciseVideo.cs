using GymPlatform.Modules.Training.Domain.Events;
using GymPlatform.Modules.Training.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Entities;

public sealed class ExerciseVideo : BaseEntity
{
    private const int MaxTitleLength = 200;
    private const int MaxDescriptionLength = 1000;
    private const int MaxVideoUrlLength = 500;

    private ExerciseVideo()
    {
    }

    public ExerciseVideo(Guid exerciseId, string title, string videoUrl, Guid coachId)
    {
        SetExerciseId(exerciseId);
        SetTitle(title);
        SetVideoUrl(videoUrl);
        SetCoachId(coachId);

        CreatedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new ExerciseVideoUploaded(Id, exerciseId, coachId));
    }

    public Guid ExerciseId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public string VideoUrl { get; private set; } = string.Empty;

    public string? ThumbnailUrl { get; private set; }

    public int? DurationSeconds { get; private set; }

    public Guid CoachId { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    public bool IsApproved { get; private set; }

    public int ViewCount { get; private set; }

    public void SetExerciseId(Guid exerciseId)
    {
        if (exerciseId == Guid.Empty)
        {
            throw new TrainingDomainException("Exercise identifier is required.");
        }

        ExerciseId = exerciseId;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new TrainingDomainException("Video title is required.");
        }

        var trimmed = title.Trim();

        if (trimmed.Length > MaxTitleLength)
        {
            throw new TrainingDomainException($"Video title cannot exceed {MaxTitleLength} characters.");
        }

        Title = trimmed;
    }

    public void SetDescription(string? description)
    {
        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim().Length <= MaxDescriptionLength
                ? description.Trim()
                : throw new TrainingDomainException($"Video description cannot exceed {MaxDescriptionLength} characters.");
    }

    public void SetVideoUrl(string videoUrl)
    {
        if (string.IsNullOrWhiteSpace(videoUrl))
        {
            throw new TrainingDomainException("Video URL is required.");
        }

        var trimmed = videoUrl.Trim();

        if (trimmed.Length > MaxVideoUrlLength)
        {
            throw new TrainingDomainException($"Video URL cannot exceed {MaxVideoUrlLength} characters.");
        }

        VideoUrl = trimmed;
    }

    public void SetThumbnailUrl(string? thumbnailUrl)
    {
        ThumbnailUrl = thumbnailUrl?.Trim();
    }

    public void SetDurationSeconds(int? durationSeconds)
    {
        DurationSeconds = durationSeconds > 0 ? durationSeconds : null;
    }

    public void SetCoachId(Guid coachId)
    {
        if (coachId == Guid.Empty)
        {
            throw new TrainingDomainException("Coach identifier is required.");
        }

        CoachId = coachId;
    }

    public void Approve()
    {
        IsApproved = true;
    }

    public void Reject()
    {
        IsApproved = false;
    }

    public void IncrementViewCount()
    {
        ViewCount++;
    }
}