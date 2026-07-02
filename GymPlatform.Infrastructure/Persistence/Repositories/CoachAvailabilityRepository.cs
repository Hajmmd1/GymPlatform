using GymPlatform.Infrastructure.Persistence;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class CoachAvailabilityRepository : ICoachAvailabilityRepository
{
    private readonly CommunicationDbContext _dbContext;

    public CoachAvailabilityRepository(CommunicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CoachAvailability?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CoachAvailabilities.FindAsync([id], cancellationToken);
    }

    public async Task AddAsync(CoachAvailability coachAvailability, CancellationToken cancellationToken = default)
    {
        await _dbContext.CoachAvailabilities.AddAsync(coachAvailability, cancellationToken);
    }

    public async Task UpdateAsync(CoachAvailability coachAvailability, CancellationToken cancellationToken = default)
    {
        _dbContext.CoachAvailabilities.Update(coachAvailability);
    }

    public async Task<IReadOnlyList<CoachAvailability>> GetByCoachIdAsync(Guid coachId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CoachAvailabilities
            .Where(ca => ca.CoachId == coachId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<CoachAvailability>> GetByDateRangeAsync(Guid coachId, DateTimeOffset start, DateTimeOffset end, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CoachAvailabilities
            .Where(ca => ca.CoachId == coachId && ca.StartTime >= start && ca.EndTime <= end)
            .ToListAsync(cancellationToken);
    }
}
