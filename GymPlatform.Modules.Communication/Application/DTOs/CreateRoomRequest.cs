namespace GymPlatform.Modules.Communication.Application.DTOs;

public class CreateRoomRequest
{
    public Guid GymId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
}
