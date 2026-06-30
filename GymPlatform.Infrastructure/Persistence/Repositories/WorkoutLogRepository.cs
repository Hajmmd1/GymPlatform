using GymPlatform.Infrastructure.Persistence;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class WorkoutLogRepository : IWorkoutLogRepository
{
    private readonly TrainingDbContext _dbContext;

    public WorkoutLogRepository(TrainingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WorkoutLog?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.WorkoutLogs.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<WorkoutLog>> GetByMemberAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.WorkoutLogs
            .Where(l => l.MemberId == memberId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<WorkoutLog>> GetByProgramAsync(Guid programId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.WorkoutLogs
            .Where(l => l.ProgramId == programId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<WorkoutLog>> GetByDateRangeAsync(Guid memberId, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken = default)
    {
        return await _dbContext.WorkoutLogs
            .Where(l => l.MemberId == memberId && l.LoggedAt >= from && l.LoggedAt <= to)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(WorkoutLog log, CancellationToken cancellationToken = default)
    {
        await _dbContext.WorkoutLogs.AddAsync(log, cancellationToken);
    }

    public async Task UpdateAsync(WorkoutLog log, CancellationToken cancellationToken = default)
    {
        _dbContext.WorkoutLogs.Update(log);
        await Task.CompletedTask;
    }
}