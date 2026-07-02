using GymPlatform.Modules.Communication.Domain.Entities;

namespace GymPlatform.Modules.Communication.Domain.Repositories;

public interface ISessionRepository
{
    Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Session session, CancellationToken cancellationToken = default);
    Task UpdateAsync(Session session, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Session>> GetByCoachIdAsync(Guid coachId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Session>> GetByRoomIdAsync(Guid roomId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Session>> GetByDateRangeAsync(Guid gymId, DateTimeOffset start, DateTimeOffset end, CancellationToken cancellationToken = default);
}
