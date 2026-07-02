using GymPlatform.Infrastructure.Persistence;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class SessionRepository : ISessionRepository
{
    private readonly CommunicationDbContext _dbContext;

    public SessionRepository(CommunicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Sessions.FindAsync([id], cancellationToken);
    }

    public async Task AddAsync(Session session, CancellationToken cancellationToken = default)
    {
        await _dbContext.Sessions.AddAsync(session, cancellationToken);
    }

    public async Task UpdateAsync(Session session, CancellationToken cancellationToken = default)
    {
        _dbContext.Sessions.Update(session);
    }

    public async Task<IReadOnlyList<Session>> GetByCoachIdAsync(Guid coachId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Sessions
            .Where(s => s.CoachId == coachId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Session>> GetByRoomIdAsync(Guid roomId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Sessions
            .Where(s => s.RoomId == roomId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Session>> GetByDateRangeAsync(Guid gymId, DateTimeOffset start, DateTimeOffset end, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Sessions
            .Where(s => s.GymId == gymId && s.StartTime >= start && s.EndTime <= end)
            .ToListAsync(cancellationToken);
    }
}
