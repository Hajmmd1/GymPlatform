using GymPlatform.Infrastructure.Persistence;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class RoomRepository : IRoomRepository
{
    private readonly CommunicationDbContext _dbContext;

    public RoomRepository(CommunicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Rooms.FindAsync([id], cancellationToken);
    }

    public async Task AddAsync(Room room, CancellationToken cancellationToken = default)
    {
        await _dbContext.Rooms.AddAsync(room, cancellationToken);
    }

    public async Task UpdateAsync(Room room, CancellationToken cancellationToken = default)
    {
        _dbContext.Rooms.Update(room);
    }

    public async Task<IReadOnlyList<Room>> GetByGymIdAsync(Guid gymId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Rooms
            .Where(r => r.GymId == gymId)
            .ToListAsync(cancellationToken);
    }
}
