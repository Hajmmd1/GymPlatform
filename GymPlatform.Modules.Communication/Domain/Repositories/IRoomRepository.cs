using GymPlatform.Modules.Communication.Domain.Entities;

namespace GymPlatform.Modules.Communication.Domain.Repositories;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Room room, CancellationToken cancellationToken = default);
    Task UpdateAsync(Room room, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Room>> GetByGymIdAsync(Guid gymId, CancellationToken cancellationToken = default);
}
