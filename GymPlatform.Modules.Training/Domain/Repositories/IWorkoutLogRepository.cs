using GymPlatform.Modules.Training.Domain.Entities;

namespace GymPlatform.Modules.Training.Domain.Repositories;

public interface IWorkoutLogRepository
{
    Task<WorkoutLog?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<WorkoutLog>> GetByMemberAsync(Guid memberId, CancellationToken cancellationToken = default);

    Task<IEnumerable<WorkoutLog>> GetByProgramAsync(Guid programId, CancellationToken cancellationToken = default);

    Task<IEnumerable<WorkoutLog>> GetByDateRangeAsync(Guid memberId, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken = default);

    Task AddAsync(WorkoutLog log, CancellationToken cancellationToken = default);

    Task UpdateAsync(WorkoutLog log, CancellationToken cancellationToken = default);
}