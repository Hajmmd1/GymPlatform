using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Repositories;
using GymPlatform.Modules.Communication.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Commands.SetCoachAvailability;

public class SetCoachAvailabilityCommandHandler : ICommandHandler<SetCoachAvailabilityCommand, CoachAvailabilityResponse>
{
    private readonly ICoachAvailabilityRepository _coachAvailabilityRepository;
    private readonly ICommunicationUnitOfWork _unitOfWork;

    public SetCoachAvailabilityCommandHandler(ICoachAvailabilityRepository coachAvailabilityRepository, ICommunicationUnitOfWork unitOfWork)
    {
        _coachAvailabilityRepository = coachAvailabilityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CoachAvailabilityResponse>> HandleAsync(SetCoachAvailabilityCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var availability = new CoachAvailability(command.CoachId, command.GymId, command.StartTime, command.EndTime);
            availability.SetAvailable(command.IsAvailable);

            await _coachAvailabilityRepository.AddAsync(availability, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<CoachAvailabilityResponse>.Success(new CoachAvailabilityResponse
            {
                Id = availability.Id,
                CoachId = availability.CoachId,
                GymId = availability.GymId,
                StartTime = availability.StartTime,
                EndTime = availability.EndTime,
                IsAvailable = availability.IsAvailable
            });
        }
        catch (CommunicationDomainException ex)
        {
            return Result<CoachAvailabilityResponse>.Failure(ex.Message);
        }
    }
}
