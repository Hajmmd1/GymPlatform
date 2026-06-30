using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Repositories;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.UploadProgressPhoto;

public sealed class UploadProgressPhotoCommandHandler : ICommandHandler<UploadProgressPhotoCommand, ProgressPhotoResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProgressPhotoRepository _progressPhotoRepository;
    private readonly ICommandValidator<UploadProgressPhotoCommand> _validator;

    public UploadProgressPhotoCommandHandler(
        IUnitOfWork unitOfWork,
        IProgressPhotoRepository progressPhotoRepository,
        ICommandValidator<UploadProgressPhotoCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _progressPhotoRepository = progressPhotoRepository;
        _validator = validator;
    }

    public async Task<Result<ProgressPhotoResponse>> HandleAsync(UploadProgressPhotoCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
        {
            return Result<ProgressPhotoResponse>.Failure(validationResult.Error);
        }

        var progressPhoto = new ProgressPhoto(command.MemberId, command.PhotoUrl, command.CapturedAt);
        progressPhoto.SetThumbnailUrl(command.ThumbnailUrl);
        progressPhoto.SetNotes(command.Notes);
        progressPhoto.SetPrivacy(command.IsPrivate);

        await _progressPhotoRepository.AddAsync(progressPhoto, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ProgressPhotoResponse>.Success(new ProgressPhotoResponse(
            progressPhoto.Id,
            progressPhoto.MemberId,
            progressPhoto.PhotoUrl,
            progressPhoto.CapturedAt,
            progressPhoto.UploadedAt,
            progressPhoto.ThumbnailUrl,
            progressPhoto.Notes,
            progressPhoto.IsPrivate,
            progressPhoto.IsApproved));
    }
}