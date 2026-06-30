using GymPlatform.Modules.Membership.Application.DTOs;
using GymPlatform.Modules.Membership.Application.Interfaces;
using GymPlatform.Modules.Membership.Domain.Repositories;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Application.Commands.DeactivateGym;

public sealed class DeactivateGymCommandHandler : ICommandHandler<DeactivateGymCommand, GymResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGymRepository _gymRepository;

    public DeactivateGymCommandHandler(
        IUnitOfWork unitOfWork,
        IGymRepository gymRepository)
    {
        _unitOfWork = unitOfWork;
        _gymRepository = gymRepository;
    }

    public async Task<Result<GymResponse>> HandleAsync(DeactivateGymCommand command, CancellationToken cancellationToken = default)
    {
        var gym = await _gymRepository.GetByIdAsync(command.GymId, cancellationToken);

        if (gym is null)
        {
            return Result<GymResponse>.Failure("Gym not found.");
        }

        gym.Deactivate();

        await _gymRepository.UpdateAsync(gym, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<GymResponse>.Success(new GymResponse(
            gym.Id,
            gym.Name,
            gym.Description,
            gym.CreatedAt,
            gym.IsActive));
    }
}