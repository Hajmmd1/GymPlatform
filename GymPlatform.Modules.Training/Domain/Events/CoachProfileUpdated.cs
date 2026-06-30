using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Events;

public sealed class CoachProfileUpdated : DomainEventBase
{
    public CoachProfileUpdated(Guid coachId, Guid profileId)
    {
        CoachId = coachId;
        ProfileId = profileId;
    }

    public Guid CoachId { get; }
    public Guid ProfileId { get; }
}