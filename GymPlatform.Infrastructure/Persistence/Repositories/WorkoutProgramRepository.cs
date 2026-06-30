using GymPlatform.Infrastructure.Persistence;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Enums;
using GymPlatform.Modules.Training.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class WorkoutProgramRepository : IWorkoutProgramRepository
{
    private readonly TrainingDbContext _dbContext;

    public WorkoutProgramRepository(TrainingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WorkoutProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.WorkoutPrograms
            .Include(p => p.Exercises)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<WorkoutProgram?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.WorkoutPrograms.FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<WorkoutProgram>> GetByCoachAsync(Guid coachId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.WorkoutPrograms
            .Where(p => p.CoachId == coachId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<WorkoutProgram>> GetByCategoryAsync(ExerciseCategory category, CancellationToken cancellationToken = default)
    {
        return await _dbContext.WorkoutPrograms
            .Where(p => p.Category == category)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(WorkoutProgram program, CancellationToken cancellationToken = default)
    {
        await _dbContext.WorkoutPrograms.AddAsync(program, cancellationToken);
    }

    public async Task UpdateAsync(WorkoutProgram program, CancellationToken cancellationToken = default)
    {
        _dbContext.WorkoutPrograms.Update(program);
        await Task.CompletedTask;
    }
}