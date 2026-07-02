using GymPlatform.Modules.Communication.Domain.Enums;

namespace GymPlatform.Modules.Communication.Application.DTOs;

public class CreateSessionRequest
{
    public Guid GymId { get; set; }
    public Guid CoachId { get; set; }
    public Guid? RoomId { get; set; }
    public string Name { get; set; } = string.Empty;
    public SessionType SessionType { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public int MaxCapacity { get; set; }
}
