using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Interfaces;

public interface ICommandHandler<in TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    Task<Result<TResult>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}