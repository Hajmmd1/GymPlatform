using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Application.Interfaces;

public interface ICommandValidator<in TCommand>
{
    Result Validate(TCommand command);
}
