using GymPlatform.Infrastructure.Persistence;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class ProgressPhotoRepository : IProgressPhotoRepository
{
    private readonly TrainingDbContext _dbContext;

    public ProgressPhotoRepository(TrainingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProgressPhoto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ProgressPhotos.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<ProgressPhoto>> GetByMemberAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ProgressPhotos
            .Where(p => p.MemberId == memberId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProgressPhoto>> GetByDateRangeAsync(Guid memberId, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ProgressPhotos
            .Where(p => p.MemberId == memberId && p.CapturedAt >= from && p.CapturedAt <= to)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProgressPhoto photo, CancellationToken cancellationToken = default)
    {
        await _dbContext.ProgressPhotos.AddAsync(photo, cancellationToken);
    }

    public async Task UpdateAsync(ProgressPhoto photo, CancellationToken cancellationToken = default)
    {
        _dbContext.ProgressPhotos.Update(photo);
        await Task.CompletedTask;
    }
}