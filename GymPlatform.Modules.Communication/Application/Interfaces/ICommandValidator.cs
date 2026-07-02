using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Interfaces;

public interface ICommandValidator<in TCommand>
{
    Result Validate(TCommand command);
}
