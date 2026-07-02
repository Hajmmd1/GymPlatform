using GymPlatform.Infrastructure.Persistence;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Enums;
using GymPlatform.Modules.Communication.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class BookingRepository : IBookingRepository
{
    private readonly CommunicationDbContext _dbContext;

    public BookingRepository(CommunicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Bookings.FindAsync([id], cancellationToken);
    }

    public async Task AddAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        await _dbContext.Bookings.AddAsync(booking, cancellationToken);
    }

    public async Task UpdateAsync(Booking booking, CancellationToken cancellationToken = default)
    {
        _dbContext.Bookings.Update(booking);
    }

    public async Task<IReadOnlyList<Booking>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Bookings
            .Where(b => b.SessionId == sessionId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Booking>> GetByMemberIdAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Bookings
            .Where(b => b.MemberId == memberId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> HasActiveBookingAsync(Guid sessionId, Guid memberId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Bookings
            .AnyAsync(b => b.SessionId == sessionId && b.MemberId == memberId && b.Status == BookingStatus.Confirmed, cancellationToken);
    }
}
