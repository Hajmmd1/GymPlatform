using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Application.Interfaces;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Repositories;
using GymPlatform.Modules.Communication.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Communication.Application.Commands.CreateRoom;

public class CreateRoomCommandHandler : ICommandHandler<CreateRoomCommand, RoomResponse>
{
    private readonly IRoomRepository _roomRepository;
    private readonly ICommunicationUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(IRoomRepository roomRepository, ICommunicationUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoomResponse>> HandleAsync(CreateRoomCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var room = new Room(command.GymId, command.Name, command.Capacity);
            await _roomRepository.AddAsync(room, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<RoomResponse>.Success(new RoomResponse
            {
                Id = room.Id,
                GymId = room.GymId,
                Name = room.Name,
                Capacity = room.Capacity,
                IsActive = room.IsActive
            });
        }
        catch (CommunicationDomainException ex)
        {
            return Result<RoomResponse>.Failure(ex.Message);
        }
    }
}
