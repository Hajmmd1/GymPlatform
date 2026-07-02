using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Repositories;
using GymPlatform.Modules.Communication.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Commands.CancelSession;

public class CancelSessionCommandHandler : ICommandHandler<CancelSessionCommand, SessionResponse>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICommunicationUnitOfWork _unitOfWork;

    public CancelSessionCommandHandler(ISessionRepository sessionRepository, ICommunicationUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SessionResponse>> HandleAsync(CancelSessionCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var session = await _sessionRepository.GetByIdAsync(command.SessionId, cancellationToken);
            if (session is null)
            {
                return Result<SessionResponse>.Failure("Session not found.");
            }

            session.CancelSession();
            await _sessionRepository.UpdateAsync(session, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<SessionResponse>.Success(new SessionResponse
            {
                Id = session.Id,
                GymId = session.GymId,
                CoachId = session.CoachId,
                RoomId = session.RoomId,
                Name = session.Name,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                MaxCapacity = session.MaxCapacity,
                BookedCount = session.BookedCount,
                IsCancelled = session.IsCancelled
            });
        }
        catch (CommunicationDomainException ex)
        {
            return Result<SessionResponse>.Failure(ex.Message);
        }
    }
}
