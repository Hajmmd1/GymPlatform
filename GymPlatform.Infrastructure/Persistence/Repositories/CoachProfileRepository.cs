using GymPlatform.Infrastructure.Persistence;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class CoachProfileRepository : ICoachProfileRepository
{
    private readonly TrainingDbContext _dbContext;

    public CoachProfileRepository(TrainingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CoachProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CoachProfiles.FindAsync([id], cancellationToken);
    }

    public async Task<CoachProfile?> GetByCoachIdAsync(Guid coachId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CoachProfiles
            .Include(p => p.Certifications)
            .FirstOrDefaultAsync(p => p.CoachId == coachId, cancellationToken);
    }

    public async Task<IEnumerable<CoachProfile>> GetBySpecialtyAsync(string specialty, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CoachProfiles
            .Where(p => p.Specialties.Any(s => s == specialty))
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CoachProfile profile, CancellationToken cancellationToken = default)
    {
        await _dbContext.CoachProfiles.AddAsync(profile, cancellationToken);
    }

    public async Task UpdateAsync(CoachProfile profile, CancellationToken cancellationToken = default)
    {
        _dbContext.CoachProfiles.Update(profile);
        await Task.CompletedTask;
    }
}