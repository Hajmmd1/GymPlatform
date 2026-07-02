using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Repositories;
using GymPlatform.Modules.Communication.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Commands.CreateSession;

public class CreateSessionCommandHandler : ICommandHandler<CreateSessionCommand, SessionResponse>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly ICommunicationUnitOfWork _unitOfWork;

    public CreateSessionCommandHandler(ISessionRepository sessionRepository, IRoomRepository roomRepository, ICommunicationUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SessionResponse>> HandleAsync(CreateSessionCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            if (command.RoomId.HasValue)
            {
                var room = await _roomRepository.GetByIdAsync(command.RoomId.Value, cancellationToken);
                if (room is null)
                {
                    return Result<SessionResponse>.Failure("Room not found.");
                }

                if (!room.IsActive)
                {
                    return Result<SessionResponse>.Failure("Room is not active.");
                }

                var existingSessions = await _sessionRepository.GetByRoomIdAsync(room.Id, cancellationToken);
                if (existingSessions.Any(s => !s.IsCancelled && s.HasOverlap(command.StartTime, command.EndTime)))
                {
                    return Result<SessionResponse>.Failure("Room is already booked for the selected time range.");
                }
            }

            var session = new Session(command.GymId, command.CoachId, command.RoomId, command.Name, command.SessionType, command.StartTime, command.EndTime, command.MaxCapacity);
            await _sessionRepository.AddAsync(session, cancellationToken);
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
