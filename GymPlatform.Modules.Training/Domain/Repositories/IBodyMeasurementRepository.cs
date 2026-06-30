using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Enums;

namespace GymPlatform.Modules.Training.Domain.Repositories;

public interface IBodyMeasurementRepository
{
    Task<BodyMeasurement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<BodyMeasurement>> GetByMemberAsync(Guid memberId, CancellationToken cancellationToken = default);

    Task<IEnumerable<BodyMeasurement>> GetByTypeAsync(Guid memberId, MeasurementType type, CancellationToken cancellationToken = default);

    Task<IEnumerable<BodyMeasurement>> GetByDateRangeAsync(Guid memberId, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken = default);

    Task<decimal?> GetLatestValueAsync(Guid memberId, MeasurementType type, CancellationToken cancellationToken = default);

    Task AddAsync(BodyMeasurement measurement, CancellationToken cancellationToken = default);

    Task UpdateAsync(BodyMeasurement measurement, CancellationToken cancellationToken = default);
}