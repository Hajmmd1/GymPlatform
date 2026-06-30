using GymPlatform.Modules.Membership.Domain.Entities;
using GymPlatform.Modules.Membership.Domain.Repositories;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class GymRepository : IGymRepository
{
    private readonly MembershipDbContext _dbContext;

    public GymRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Gym?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Gyms.FindAsync([id], cancellationToken);
    }

    public async Task AddAsync(Gym gym, CancellationToken cancellationToken = default)
    {
        await _dbContext.Gyms.AddAsync(gym, cancellationToken);
    }

    public async Task UpdateAsync(Gym gym, CancellationToken cancellationToken = default)
    {
        _dbContext.Gyms.Update(gym);
    }
}