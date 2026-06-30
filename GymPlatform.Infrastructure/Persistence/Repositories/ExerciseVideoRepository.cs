using GymPlatform.Infrastructure.Persistence;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class ExerciseVideoRepository : IExerciseVideoRepository
{
    private readonly TrainingDbContext _dbContext;

    public ExerciseVideoRepository(TrainingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ExerciseVideo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ExerciseVideos.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<ExerciseVideo>> GetByExerciseAsync(Guid exerciseId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ExerciseVideos
            .Where(v => v.ExerciseId == exerciseId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ExerciseVideo>> GetByCoachAsync(Guid coachId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ExerciseVideos
            .Where(v => v.CoachId == coachId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ExerciseVideo video, CancellationToken cancellationToken = default)
    {
        await _dbContext.ExerciseVideos.AddAsync(video, cancellationToken);
    }

    public async Task UpdateAsync(ExerciseVideo video, CancellationToken cancellationToken = default)
    {
        _dbContext.ExerciseVideos.Update(video);
        await Task.CompletedTask;
    }
}