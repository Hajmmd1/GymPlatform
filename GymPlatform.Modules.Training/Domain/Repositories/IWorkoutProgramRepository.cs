using GymPlatform.Modules.Training.Domain.Entities;

namespace GymPlatform.Modules.Training.Domain.Repositories;

public interface IWorkoutProgramRepository
{
    Task<WorkoutProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<WorkoutProgram?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<IEnumerable<WorkoutProgram>> GetByCoachAsync(Guid coachId, CancellationToken cancellationToken = default);

    Task<IEnumerable<WorkoutProgram>> GetByCategoryAsync(Domain.Enums.ExerciseCategory category, CancellationToken cancellationToken = default);

    Task AddAsync(WorkoutProgram program, CancellationToken cancellationToken = default);

    Task UpdateAsync(WorkoutProgram program, CancellationToken cancellationToken = default);
}