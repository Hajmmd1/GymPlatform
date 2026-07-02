namespace GymPlatform.Modules.Communication.Application.DTOs;

public class SetCoachAvailabilityRequest
{
    public Guid CoachId { get; set; }
    public Guid GymId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public bool IsAvailable { get; set; }
}
