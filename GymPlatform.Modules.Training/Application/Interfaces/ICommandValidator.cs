using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Interfaces;

public interface ICommandValidator<in TCommand>
{
    Result Validate(TCommand command);
}