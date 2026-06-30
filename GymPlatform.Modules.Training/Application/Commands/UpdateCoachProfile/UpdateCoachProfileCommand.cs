using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;

namespace GymPlatform.Modules.Training.Application.Commands.UpdateCoachProfile;

public sealed class UpdateCoachProfileCommand : ICommand<CoachProfileResponse>
{
    public UpdateCoachProfileCommand(Guid coachId, string? bio, string? profilePhotoUrl, List<string>? specialties)
    {
        CoachId = coachId;
        Bio = bio;
        ProfilePhotoUrl = profilePhotoUrl;
        Specialties = specialties;
    }

    public Guid CoachId { get; }
    public string? Bio { get; }
    public string? ProfilePhotoUrl { get; }
    public List<string>? Specialties { get; }
}