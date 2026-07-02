namespace GymPlatform.Modules.Communication.Domain.Repositories;

public interface ICommunicationUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
