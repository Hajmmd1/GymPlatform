using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Domain.Events;

public sealed class BookingCreated : DomainEventBase
{
    public BookingCreated(Guid bookingId, Guid sessionId, Guid memberId)
    {
        BookingId = bookingId;
        SessionId = sessionId;
        MemberId = memberId;
    }

    public Guid BookingId { get; }
    public Guid SessionId { get; }
    public Guid MemberId { get; }
}
