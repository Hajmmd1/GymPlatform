using GymPlatform.Modules.Communication.Domain.Enums;
using GymPlatform.Modules.Communication.Domain.Events;
using GymPlatform.Modules.Communication.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Domain.Entities;

public sealed class Booking : BaseEntity
{
    private Booking()
    {
    }

    public Booking(Guid sessionId, Guid memberId)
    {
        if (sessionId == Guid.Empty)
        {
            throw new CommunicationDomainException("Session identifier is required.");
        }

        if (memberId == Guid.Empty)
        {
            throw new CommunicationDomainException("Member identifier is required.");
        }

        SessionId = sessionId;
        MemberId = memberId;
        Status = BookingStatus.Confirmed;
        CreatedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new BookingCreated(Id, sessionId, memberId));
    }

    public Guid SessionId { get; private set; }

    public Guid MemberId { get; private set; }

    public BookingStatus Status { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset? CancelledAt { get; private set; }

    public void Cancel()
    {
        if (Status == BookingStatus.Cancelled)
        {
            throw new CommunicationDomainException("Booking is already cancelled.");
        }

        Status = BookingStatus.Cancelled;
        CancelledAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new BookingCancelled(Id, SessionId));
    }
}
