using Xunit;
using GymPlatform.Modules.Communication.Application.Commands.CreateSession;
using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Enums;
using GymPlatform.Modules.Communication.Domain.Repositories;
using GymPlatform.SharedKernel;
using Moq;

namespace GymPlatform.Modules.Communication.Tests.Application.Commands.CreateSession;

public class CreateSessionCommandHandlerTests
{
    [Fact]
    public async Task Handle_Valid_Command_Creates_Session()
    {
        var sessionRepositoryMock = new Mock<ISessionRepository>();
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var unitOfWorkMock = new Mock<GymPlatform.Modules.Communication.Domain.Repositories.ICommunicationUnitOfWork>();

        sessionRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Session>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        roomRepositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Room?)null);

        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1))
            .Verifiable();

        var handler = new CreateSessionCommandHandler(
            sessionRepositoryMock.Object,
            roomRepositoryMock.Object,
            unitOfWorkMock.Object,
            new CreateSessionCommandValidator());

        var command = new CreateSessionCommand(
            Guid.NewGuid(), Guid.NewGuid(), null,
            "Morning HIIT", SessionType.Class,
            DateTimeOffset.UtcNow.AddDays(1), DateTimeOffset.UtcNow.AddDays(1).AddHours(1), 15);

        var result = await handler.HandleAsync(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("Morning HIIT", result.Value!.Name);

        sessionRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Session>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Room_Not_Found_Returns_Failure()
    {
        var sessionRepositoryMock = new Mock<ISessionRepository>();
        var roomRepositoryMock = new Mock<IRoomRepository>();
        var unitOfWorkMock = new Mock<GymPlatform.Modules.Communication.Domain.Repositories.ICommunicationUnitOfWork>();

        var handler = new CreateSessionCommandHandler(
            sessionRepositoryMock.Object,
            roomRepositoryMock.Object,
            unitOfWorkMock.Object,
            new CreateSessionCommandValidator());

        var command = new CreateSessionCommand(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
            "Morning HIIT", SessionType.Class,
            DateTimeOffset.UtcNow.AddDays(1), DateTimeOffset.UtcNow.AddDays(1).AddHours(1), 15);

        var result = await handler.HandleAsync(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Contains("Room not found", result.Error);
    }
}
