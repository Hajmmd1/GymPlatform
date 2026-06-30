using GymPlatform.Modules.Membership.Domain.Entities;

namespace GymPlatform.Modules.Membership.Domain.Repositories;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Member?> GetByEmailAndGymIdAsync(string email, Guid gymId, CancellationToken cancellationToken = default);

    Task AddAsync(Member member, CancellationToken cancellationToken = default);

    Task UpdateAsync(Member member, CancellationToken cancellationToken = default);
}
