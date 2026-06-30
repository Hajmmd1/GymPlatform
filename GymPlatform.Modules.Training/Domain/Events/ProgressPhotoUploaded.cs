using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Events;

public sealed class ProgressPhotoUploaded : DomainEventBase
{
    public ProgressPhotoUploaded(Guid photoId, Guid memberId)
    {
        PhotoId = photoId;
        MemberId = memberId;
    }

    public Guid PhotoId { get; }
    public Guid MemberId { get; }
}