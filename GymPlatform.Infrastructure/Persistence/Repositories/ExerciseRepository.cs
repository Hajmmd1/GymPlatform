using GymPlatform.Infrastructure.Persistence;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class ExerciseRepository : IExerciseRepository
{
    private readonly TrainingDbContext _dbContext;

    public ExerciseRepository(TrainingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Exercise?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Exercises.FindAsync([id], cancellationToken);
    }

    public async Task<Exercise?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Exercises.FirstOrDefaultAsync(e => e.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Exercise>> GetByMuscleGroupAsync(string muscleGroup, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Exercises
            .Where(e => e.PrimaryMuscleGroups.Any(mg => mg.Value == muscleGroup) ||
                        e.SecondaryMuscleGroups.Any(mg => mg.Value == muscleGroup))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Exercise>> GetByCoachAsync(Guid coachId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Exercises
            .Where(e => e.CoachId == coachId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Exercise exercise, CancellationToken cancellationToken = default)
    {
        await _dbContext.Exercises.AddAsync(exercise, cancellationToken);
    }

    public async Task UpdateAsync(Exercise exercise, CancellationToken cancellationToken = default)
    {
        _dbContext.Exercises.Update(exercise);
        await Task.CompletedTask;
    }
}