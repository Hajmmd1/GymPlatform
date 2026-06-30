using GymPlatform.Modules.Training.Domain.Entities;

namespace GymPlatform.Modules.Training.Domain.Repositories;

public interface IExerciseVideoRepository
{
    Task<ExerciseVideo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<ExerciseVideo>> GetByExerciseAsync(Guid exerciseId, CancellationToken cancellationToken = default);

    Task<IEnumerable<ExerciseVideo>> GetByCoachAsync(Guid coachId, CancellationToken cancellationToken = default);

    Task AddAsync(ExerciseVideo video, CancellationToken cancellationToken = default);

    Task UpdateAsync(ExerciseVideo video, CancellationToken cancellationToken = default);
}