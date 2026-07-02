using GymPlatform.Modules.Communication.Domain.Events;
using GymPlatform.Modules.Communication.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Domain.Entities;

public sealed class Room : BaseEntity
{
    private const int MaxNameLength = 200;

    private Room()
    {
    }

    public Room(Guid gymId, string name, int capacity)
    {
        EnsureGymId(gymId);
        SetName(name);
        EnsureCapacity(capacity);

        GymId = gymId;
        Capacity = capacity;
        IsActive = true;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid GymId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public int Capacity { get; private set; }

    public bool IsActive { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new CommunicationDomainException("Room name is required.");
        }

        var trimmed = name.Trim();

        if (trimmed.Length > MaxNameLength)
        {
            throw new CommunicationDomainException($"Room name cannot exceed {MaxNameLength} characters.");
        }

        Name = trimmed;
    }

    public void SetCapacity(int capacity)
    {
        EnsureCapacity(capacity);
        Capacity = capacity;
    }

    public void Deactivate()
    {
        if (!IsActive)
        {
            throw new CommunicationDomainException("Room is already inactive.");
        }

        IsActive = false;
    }

    public void Activate()
    {
        if (IsActive)
        {
            throw new CommunicationDomainException("Room is already active.");
        }

        IsActive = true;
    }

    private static void EnsureGymId(Guid gymId)
    {
        if (gymId == Guid.Empty)
        {
            throw new CommunicationDomainException("Gym identifier is required.");
        }
    }

    private static void EnsureCapacity(int capacity)
    {
        if (capacity < 1)
        {
            throw new CommunicationDomainException("Capacity must be at least 1.");
        }
    }
}
