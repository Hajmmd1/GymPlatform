namespace GymPlatform.Modules.Communication.Application.DTOs;

public class RoomResponse
{
    public Guid Id { get; set; }
    public Guid GymId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
}
