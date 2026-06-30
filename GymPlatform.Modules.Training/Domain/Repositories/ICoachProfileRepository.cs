using GymPlatform.Modules.Training.Domain.Entities;

namespace GymPlatform.Modules.Training.Domain.Repositories;

public interface ICoachProfileRepository
{
    Task<CoachProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<CoachProfile?> GetByCoachIdAsync(Guid coachId, CancellationToken cancellationToken = default);

    Task<IEnumerable<CoachProfile>> GetBySpecialtyAsync(string specialty, CancellationToken cancellationToken = default);

    Task AddAsync(CoachProfile profile, CancellationToken cancellationToken = default);

    Task UpdateAsync(CoachProfile profile, CancellationToken cancellationToken = default);
}