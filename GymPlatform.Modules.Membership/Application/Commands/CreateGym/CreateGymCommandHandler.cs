using GymPlatform.Modules.Membership.Application.DTOs;
using GymPlatform.Modules.Membership.Application.Interfaces;
using GymPlatform.Modules.Membership.Domain.Entities;
using GymPlatform.Modules.Membership.Domain.Repositories;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Application.Commands.CreateGym;

public sealed class CreateGymCommandHandler : ICommandHandler<CreateGymCommand, GymResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGymRepository _gymRepository;
    private readonly ICommandValidator<CreateGymCommand> _validator;

    public CreateGymCommandHandler(
        IUnitOfWork unitOfWork,
        IGymRepository gymRepository,
        ICommandValidator<CreateGymCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _gymRepository = gymRepository;
        _validator = validator;
    }

    public async Task<Result<GymResponse>> HandleAsync(CreateGymCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
        {
            return Result<GymResponse>.Failure(validationResult.Error);
        }

        var gym = new Gym(command.Name, command.Description);

        await _gymRepository.AddAsync(gym, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<GymResponse>.Success(new GymResponse(
            gym.Id,
            gym.Name,
            gym.Description,
            gym.CreatedAt,
            gym.IsActive));
    }
}