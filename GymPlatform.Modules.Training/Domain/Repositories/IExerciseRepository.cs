using GymPlatform.Modules.Training.Domain.Entities;

namespace GymPlatform.Modules.Training.Domain.Repositories;

public interface IExerciseRepository
{
    Task<Exercise?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Exercise?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<IEnumerable<Exercise>> GetByMuscleGroupAsync(string muscleGroup, CancellationToken cancellationToken = default);

    Task<IEnumerable<Exercise>> GetByCoachAsync(Guid coachId, CancellationToken cancellationToken = default);

    Task AddAsync(Exercise exercise, CancellationToken cancellationToken = default);

    Task UpdateAsync(Exercise exercise, CancellationToken cancellationToken = default);
}