namespace GymPlatform.Modules.Communication.Application.DTOs;

public class BookingResponse
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public Guid MemberId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
