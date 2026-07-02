using Xunit;
using GymPlatform.Modules.Communication.Application.Commands.CreateRoom;
using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Repositories;
using GymPlatform.SharedKernel;
using Moq;

namespace GymPlatform.Modules.Communication.Tests.Application.Commands.CreateRoom;

public class CreateRoomCommandHandlerTests
{
    [Fact]
    public async Task Handle_Valid_Command_Creates_Room()
    {
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var unitOfWorkMock = new Mock<GymPlatform.Modules.Communication.Domain.Repositories.ICommunicationUnitOfWork>();

        roomRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1))
            .Verifiable();

        var handler = new CreateRoomCommandHandler(
            roomRepositoryMock.Object,
            unitOfWorkMock.Object,
            new CreateRoomCommandValidator());

        var command = new CreateRoomCommand(Guid.NewGuid(), "Studio A", 20);
        var result = await handler.HandleAsync(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("Studio A", result.Value!.Name);
        Assert.Equal(20, result.Value.Capacity);

        roomRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Invalid_Command_Returns_Failure()
    {
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var unitOfWorkMock = new Mock<GymPlatform.Modules.Communication.Domain.Repositories.ICommunicationUnitOfWork>();

        var handler = new CreateRoomCommandHandler(
            roomRepositoryMock.Object,
            unitOfWorkMock.Object,
            new CreateRoomCommandValidator());

        var command = new CreateRoomCommand(Guid.Empty, string.Empty, 0);
        var result = await handler.HandleAsync(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Contains("Gym identifier is required", result.Error);

        roomRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
