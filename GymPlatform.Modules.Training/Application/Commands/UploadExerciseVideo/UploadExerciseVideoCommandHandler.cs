using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Repositories;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.UploadExerciseVideo;

public sealed class UploadExerciseVideoCommandHandler : ICommandHandler<UploadExerciseVideoCommand, ExerciseVideoResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExerciseVideoRepository _exerciseVideoRepository;
    private readonly ICommandValidator<UploadExerciseVideoCommand> _validator;

    public UploadExerciseVideoCommandHandler(
        IUnitOfWork unitOfWork,
        IExerciseVideoRepository exerciseVideoRepository,
        ICommandValidator<UploadExerciseVideoCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _exerciseVideoRepository = exerciseVideoRepository;
        _validator = validator;
    }

    public async Task<Result<ExerciseVideoResponse>> HandleAsync(UploadExerciseVideoCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
        {
            return Result<ExerciseVideoResponse>.Failure(validationResult.Error);
        }

        var video = new ExerciseVideo(command.ExerciseId, command.Title, command.VideoUrl, command.CoachId);
        video.SetDescription(command.Description);
        video.SetThumbnailUrl(command.ThumbnailUrl);
        video.SetDurationSeconds(command.DurationSeconds);

        await _exerciseVideoRepository.AddAsync(video, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ExerciseVideoResponse>.Success(new ExerciseVideoResponse(
            video.Id,
            video.ExerciseId,
            video.Title,
            video.Description,
            video.VideoUrl,
            video.ThumbnailUrl,
            video.DurationSeconds,
            video.CoachId,
            video.CreatedAt,
            video.IsApproved,
            video.ViewCount));
    }
}