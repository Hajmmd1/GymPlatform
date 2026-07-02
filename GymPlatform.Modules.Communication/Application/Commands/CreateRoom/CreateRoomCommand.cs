using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Application.Interfaces;

namespace GymPlatform.Modules.Communication.Application.Commands.CreateRoom;

public record CreateRoomCommand(Guid GymId, string Name, int Capacity) : ICommand<RoomResponse>;
