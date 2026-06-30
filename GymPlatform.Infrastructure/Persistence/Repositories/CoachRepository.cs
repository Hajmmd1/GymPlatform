using GymPlatform.Modules.Membership.Domain.Entities;
using GymPlatform.Modules.Membership.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class CoachRepository : ICoachRepository
{
    private readonly MembershipDbContext _dbContext;

    public CoachRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Coach?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Coaches.FindAsync([id], cancellationToken);
    }

    public async Task<Coach?> GetByEmailAndGymIdAsync(string email, Guid gymId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Coaches
            .FirstOrDefaultAsync(c => c.Email.Value == email && c.GymId == gymId, cancellationToken);
    }

    public async Task AddAsync(Coach coach, CancellationToken cancellationToken = default)
    {
        await _dbContext.Coaches.AddAsync(coach, cancellationToken);
    }
}