using GymPlatform.Infrastructure.Persistence;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Enums;
using GymPlatform.Modules.Training.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class BodyMeasurementRepository : IBodyMeasurementRepository
{
    private readonly TrainingDbContext _dbContext;

    public BodyMeasurementRepository(TrainingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BodyMeasurement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.BodyMeasurements.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<BodyMeasurement>> GetByMemberAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.BodyMeasurements
            .Where(m => m.MemberId == memberId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<BodyMeasurement>> GetByTypeAsync(Guid memberId, MeasurementType type, CancellationToken cancellationToken = default)
    {
        return await _dbContext.BodyMeasurements
            .Where(m => m.MemberId == memberId && m.Type == type)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<BodyMeasurement>> GetByDateRangeAsync(Guid memberId, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken = default)
    {
        return await _dbContext.BodyMeasurements
            .Where(m => m.MemberId == memberId && m.MeasuredAt >= from && m.MeasuredAt <= to)
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal?> GetLatestValueAsync(Guid memberId, MeasurementType type, CancellationToken cancellationToken = default)
    {
        return await _dbContext.BodyMeasurements
            .Where(m => m.MemberId == memberId && m.Type == type)
            .MaxAsync(m => (decimal?)m.Value, cancellationToken);
    }

    public async Task AddAsync(BodyMeasurement measurement, CancellationToken cancellationToken = default)
    {
        await _dbContext.BodyMeasurements.AddAsync(measurement, cancellationToken);
    }

    public async Task UpdateAsync(BodyMeasurement measurement, CancellationToken cancellationToken = default)
    {
        _dbContext.BodyMeasurements.Update(measurement);
        await Task.CompletedTask;
    }
}