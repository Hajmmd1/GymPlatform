using GymPlatform.Modules.Membership.Domain.Entities;
using GymPlatform.Modules.Membership.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymPlatform.Infrastructure.Persistence.Repositories;

public sealed class MemberRepository : IMemberRepository
{
    private readonly MembershipDbContext _dbContext;

    public MemberRepository(MembershipDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Members.FindAsync([id], cancellationToken);
    }

    public async Task<Member?> GetByEmailAndGymIdAsync(string email, Guid gymId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Members
            .FirstOrDefaultAsync(m => m.Email.Value == email && m.GymId == gymId, cancellationToken);
    }

    public async Task AddAsync(Member member, CancellationToken cancellationToken = default)
    {
        await _dbContext.Members.AddAsync(member, cancellationToken);
    }

    public async Task UpdateAsync(Member member, CancellationToken cancellationToken = default)
    {
        _dbContext.Members.Update(member);
    }
}