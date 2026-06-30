using GymPlatform.Modules.Membership.Domain.Entities;

namespace GymPlatform.Modules.Membership.Domain.Repositories;

public interface ICoachRepository
{
    Task<Coach?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Coach?> GetByEmailAndGymIdAsync(string email, Guid gymId, CancellationToken cancellationToken = default);

    Task AddAsync(Coach coach, CancellationToken cancellationToken = default);
}
