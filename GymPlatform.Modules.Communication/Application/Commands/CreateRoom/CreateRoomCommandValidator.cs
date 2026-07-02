using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Commands.CreateRoom;

public class CreateRoomCommandValidator : ICommandValidator<CreateRoomCommand>
{
    public Result Validate(CreateRoomCommand command)
    {
        if (command.GymId == Guid.Empty)
        {
            return Result.Failure("Gym identifier is required.");
        }

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Failure("Room name is required.");
        }

        if (command.Capacity < 1)
        {
            return Result.Failure("Capacity must be at least 1.");
        }

        return Result.Success();
    }
}
