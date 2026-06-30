using GymPlatform.Modules.Training.Domain.Entities;

namespace GymPlatform.Modules.Training.Domain.Repositories;

public interface IProgressPhotoRepository
{
    Task<ProgressPhoto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<ProgressPhoto>> GetByMemberAsync(Guid memberId, CancellationToken cancellationToken = default);

    Task<IEnumerable<ProgressPhoto>> GetByDateRangeAsync(Guid memberId, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken = default);

    Task AddAsync(ProgressPhoto photo, CancellationToken cancellationToken = default);

    Task UpdateAsync(ProgressPhoto photo, CancellationToken cancellationToken = default);
}