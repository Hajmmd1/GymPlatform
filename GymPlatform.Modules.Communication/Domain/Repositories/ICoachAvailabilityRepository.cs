using GymPlatform.Modules.Communication.Domain.Entities;

namespace GymPlatform.Modules.Communication.Domain.Repositories;

public interface ICoachAvailabilityRepository
{
    Task<CoachAvailability?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(CoachAvailability coachAvailability, CancellationToken cancellationToken = default);
    Task UpdateAsync(CoachAvailability coachAvailability, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CoachAvailability>> GetByCoachIdAsync(Guid coachId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CoachAvailability>> GetByDateRangeAsync(Guid coachId, DateTimeOffset start, DateTimeOffset end, CancellationToken cancellationToken = default);
}
