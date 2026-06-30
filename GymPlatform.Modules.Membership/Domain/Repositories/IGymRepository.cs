using GymPlatform.Modules.Membership.Domain.Entities;

namespace GymPlatform.Modules.Membership.Domain.Repositories;

public interface IGymRepository
{
    Task<Gym?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(Gym gym, CancellationToken cancellationToken = default);

    Task UpdateAsync(Gym gym, CancellationToken cancellationToken = default);
}
