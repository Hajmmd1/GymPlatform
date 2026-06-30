using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.UploadExerciseVideo;

public sealed class UploadExerciseVideoCommandValidator : ICommandValidator<UploadExerciseVideoCommand>
{
    public Result Validate(UploadExerciseVideoCommand command)
    {
        if (command == null)
        {
            return Result.Failure("Command cannot be null.");
        }

        if (command.ExerciseId == Guid.Empty)
        {
            return Result.Failure("Exercise identifier is required.");
        }

        if (string.IsNullOrWhiteSpace(command.Title))
        {
            return Result.Failure("Video title is required.");
        }

        if (string.IsNullOrWhiteSpace(command.VideoUrl))
        {
            return Result.Failure("Video URL is required.");
        }

        if (command.CoachId == Guid.Empty)
        {
            return Result.Failure("Coach identifier is required.");
        }

        return Result.Success();
    }
}