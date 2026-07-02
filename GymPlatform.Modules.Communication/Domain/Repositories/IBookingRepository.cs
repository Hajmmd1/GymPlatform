using GymPlatform.Modules.Communication.Domain.Entities;

namespace GymPlatform.Modules.Communication.Domain.Repositories;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Booking booking, CancellationToken cancellationToken = default);
    Task UpdateAsync(Booking booking, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Booking>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Booking>> GetByMemberIdAsync(Guid memberId, CancellationToken cancellationToken = default);
    Task<bool> HasActiveBookingAsync(Guid sessionId, Guid memberId, CancellationToken cancellationToken = default);
}
