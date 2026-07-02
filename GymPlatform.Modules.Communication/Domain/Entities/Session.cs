using GymPlatform.Modules.Communication.Domain.Enums;
using GymPlatform.Modules.Communication.Domain.Events;
using GymPlatform.Modules.Communication.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Domain.Entities;

public sealed class Session : BaseEntity
{
    private const int MaxNameLength = 200;

    private readonly List<Booking> _bookings = new();

    private Session()
    {
    }

    public Session(Guid gymId, Guid coachId, Guid? roomId, string name, SessionType sessionType, DateTimeOffset startTime, DateTimeOffset endTime, int maxCapacity)
    {
        EnsureGymId(gymId);
        EnsureCoachId(coachId);
        EnsureTimeRange(startTime, endTime);
        EnsureCapacity(maxCapacity);

        GymId = gymId;
        CoachId = coachId;
        RoomId = roomId;
        SetName(name);
        SessionType = sessionType;
        StartTime = startTime;
        EndTime = endTime;
        MaxCapacity = maxCapacity;
        BookedCount = 0;
        IsCancelled = false;

        CreatedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new SessionCreated(Id, gymId, coachId, name, startTime));
    }

    public Guid GymId { get; private set; }

    public Guid CoachId { get; private set; }

    public Guid? RoomId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public SessionType SessionType { get; private set; }

    public DateTimeOffset StartTime { get; private set; }

    public DateTimeOffset EndTime { get; private set; }

    public int MaxCapacity { get; private set; }

    public int BookedCount { get; private set; }

    public bool IsCancelled { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new CommunicationDomainException("Session name is required.");
        }

        var trimmed = name.Trim();

        if (trimmed.Length > MaxNameLength)
        {
            throw new CommunicationDomainException($"Session name cannot exceed {MaxNameLength} characters.");
        }

        Name = trimmed;
    }

    public void Book(Guid memberId)
    {
        if (IsCancelled)
        {
            throw new CommunicationDomainException("Cannot book a cancelled session.");
        }

        if (BookedCount >= MaxCapacity)
        {
            throw new CommunicationDomainException("Session has reached maximum capacity.");
        }

        if (_bookings.Any(b => b.MemberId == memberId && b.Status == BookingStatus.Confirmed))
        {
            throw new CommunicationDomainException("Member already has a confirmed booking for this session.");
        }

        var booking = new Booking(Id, memberId);
        _bookings.Add(booking);
        BookedCount++;
    }

    public void Cancel(Booking booking)
    {
        if (booking is null)
        {
            throw new CommunicationDomainException("Booking is required.");
        }

        if (!_bookings.Contains(booking))
        {
            throw new CommunicationDomainException("Booking does not belong to this session.");
        }

        booking.Cancel();
        BookedCount--;
    }

    public void CancelSession()
    {
        if (IsCancelled)
        {
            throw new CommunicationDomainException("Session is already cancelled.");
        }

        IsCancelled = true;
        AddDomainEvent(new SessionCancelled(Id, GymId));
    }

    public bool HasOverlap(DateTimeOffset startTime, DateTimeOffset endTime)
    {
        return StartTime < endTime && EndTime > startTime;
    }

    private static void EnsureGymId(Guid gymId)
    {
        if (gymId == Guid.Empty)
        {
            throw new CommunicationDomainException("Gym identifier is required.");
        }
    }

    private static void EnsureCoachId(Guid coachId)
    {
        if (coachId == Guid.Empty)
        {
            throw new CommunicationDomainException("Coach identifier is required.");
        }
    }

    private static void EnsureTimeRange(DateTimeOffset startTime, DateTimeOffset endTime)
    {
        if (endTime <= startTime)
        {
            throw new CommunicationDomainException("End time must be after start time.");
        }
    }

    private static void EnsureCapacity(int maxCapacity)
    {
        if (maxCapacity < 1)
        {
            throw new CommunicationDomainException("Maximum capacity must be at least 1.");
        }
    }
}
