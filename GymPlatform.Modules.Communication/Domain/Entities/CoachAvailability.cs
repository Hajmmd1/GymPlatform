using GymPlatform.Modules.Communication.Domain.Events;
using GymPlatform.Modules.Communication.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Domain.Entities;

public sealed class CoachAvailability : BaseEntity
{
    private CoachAvailability()
    {
    }

    public CoachAvailability(Guid coachId, Guid gymId, DateTimeOffset startTime, DateTimeOffset endTime)
    {
        EnsureCoachId(coachId);
        EnsureGymId(gymId);
        EnsureTimeRange(startTime, endTime);

        CoachId = coachId;
        GymId = gymId;
        StartTime = startTime;
        EndTime = endTime;
        IsAvailable = true;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid CoachId { get; private set; }

    public Guid GymId { get; private set; }

    public DateTimeOffset StartTime { get; private set; }

    public DateTimeOffset EndTime { get; private set; }

    public bool IsAvailable { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public void SetAvailable(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }

    public bool HasOverlap(DateTimeOffset startTime, DateTimeOffset endTime)
    {
        return StartTime < endTime && EndTime > startTime;
    }

    private static void EnsureCoachId(Guid coachId)
    {
        if (coachId == Guid.Empty)
        {
            throw new CommunicationDomainException("Coach identifier is required.");
        }
    }

    private static void EnsureGymId(Guid gymId)
    {
        if (gymId == Guid.Empty)
        {
            throw new CommunicationDomainException("Gym identifier is required.");
        }
    }

    private static void EnsureTimeRange(DateTimeOffset startTime, DateTimeOffset endTime)
    {
        if (endTime <= startTime)
        {
            throw new CommunicationDomainException("End time must be after start time.");
        }
    }
}
