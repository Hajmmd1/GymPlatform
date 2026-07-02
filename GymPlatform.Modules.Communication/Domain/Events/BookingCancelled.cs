using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Domain.Events;

public sealed class BookingCancelled : DomainEventBase
{
    public BookingCancelled(Guid bookingId, Guid sessionId)
    {
        BookingId = bookingId;
        SessionId = sessionId;
    }

    public Guid BookingId { get; }
    public Guid SessionId { get; }
}
