namespace GymPlatform.Modules.Communication.Application.DTOs;

public class SessionResponse
{
    public Guid Id { get; set; }
    public Guid GymId { get; set; }
    public Guid CoachId { get; set; }
    public Guid? RoomId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public int MaxCapacity { get; set; }
    public int BookedCount { get; set; }
    public bool IsCancelled { get; set; }
}
