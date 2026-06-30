using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.UploadProgressPhoto;

public sealed class UploadProgressPhotoCommandValidator : ICommandValidator<UploadProgressPhotoCommand>
{
    public Result Validate(UploadProgressPhotoCommand command)
    {
        if (command == null)
        {
            return Result.Failure("Command cannot be null.");
        }

        if (command.MemberId == Guid.Empty)
        {
            return Result.Failure("Member identifier is required.");
        }

        if (string.IsNullOrWhiteSpace(command.PhotoUrl))
        {
            return Result.Failure("Photo URL is required.");
        }

        return Result.Success();
    }
}