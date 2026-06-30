using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Repositories;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.UpdateCoachProfile;

public sealed class UpdateCoachProfileCommandHandler : ICommandHandler<UpdateCoachProfileCommand, CoachProfileResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICoachProfileRepository _coachProfileRepository;
    private readonly ICommandValidator<UpdateCoachProfileCommand> _validator;

    public UpdateCoachProfileCommandHandler(
        IUnitOfWork unitOfWork,
        ICoachProfileRepository coachProfileRepository,
        ICommandValidator<UpdateCoachProfileCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _coachProfileRepository = coachProfileRepository;
        _validator = validator;
    }

    public async Task<Result<CoachProfileResponse>> HandleAsync(UpdateCoachProfileCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
        {
            return Result<CoachProfileResponse>.Failure(validationResult.Error);
        }

        var existingProfile = await _coachProfileRepository.GetByCoachIdAsync(command.CoachId, cancellationToken);
        
        if (existingProfile == null)
        {
            var profile = new CoachProfile(command.CoachId);
            ApplyUpdates(profile, command);
            
            await _coachProfileRepository.AddAsync(profile, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<CoachProfileResponse>.Success(MapToResponse(profile));
        }
        
        ApplyUpdates(existingProfile, command);
        
        await _coachProfileRepository.UpdateAsync(existingProfile, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<CoachProfileResponse>.Success(MapToResponse(existingProfile));
    }

    private static void ApplyUpdates(CoachProfile profile, UpdateCoachProfileCommand command)
    {
        profile.SetBio(command.Bio);
        profile.SetProfilePhotoUrl(command.ProfilePhotoUrl);

        if (command.Specialties != null)
        {
            foreach (var specialty in command.Specialties)
            {
                profile.AddSpecialty(specialty);
            }
        }
    }

    private static CoachProfileResponse MapToResponse(CoachProfile profile)
    {
        return new CoachProfileResponse(
            profile.Id,
            profile.CoachId,
            profile.Bio,
            profile.ProfilePhotoUrl,
            profile.Specialties,
            profile.Rating,
            profile.RatingCount,
            profile.CreatedAt,
            profile.UpdatedAt,
            profile.IsActive);
    }
}