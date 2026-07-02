using Xunit;
using GymPlatform.Modules.Communication.Application.Commands.BookSession;
using GymPlatform.Modules.Communication.Application.DTOs;
using GymPlatform.Modules.Communication.Domain.Entities;
using GymPlatform.Modules.Communication.Domain.Repositories;
using GymPlatform.SharedKernel;
using Moq;

namespace GymPlatform.Modules.Communication.Tests.Application.Commands.BookSession;

public class BookSessionCommandHandlerTests
{
    [Fact]
    public async Task Handle_Valid_Command_Books_Session()
    {
        var sessionRepositoryMock = new Mock<ISessionRepository>();
        var bookingRepositoryMock = new Mock<IBookingRepository>();
        var unitOfWorkMock = new Mock<GymPlatform.Modules.Communication.Domain.Repositories.ICommunicationUnitOfWork>();

        var session = new Session(Guid.NewGuid(), Guid.NewGuid(), null, "Test", GymPlatform.Modules.Communication.Domain.Enums.SessionType.Class, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddHours(1), 10);

        sessionRepositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(session);

        bookingRepositoryMock
            .Setup(r => r.HasActiveBookingAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        bookingRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1))
            .Verifiable();

        var handler = new BookSessionCommandHandler(
            sessionRepositoryMock.Object,
            bookingRepositoryMock.Object,
            unitOfWorkMock.Object,
            new BookSessionCommandValidator());

        var command = new BookSessionCommand(session.Id, Guid.NewGuid());
        var result = await handler.HandleAsync(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_Invalid_Command_Returns_Failure()
    {
        var handler = new BookSessionCommandHandler(
            Mock.Of<ISessionRepository>(),
            Mock.Of<IBookingRepository>(),
            Mock.Of<GymPlatform.Modules.Communication.Domain.Repositories.ICommunicationUnitOfWork>(),
            new BookSessionCommandValidator());

        var command = new BookSessionCommand(Guid.Empty, Guid.Empty);
        var result = await handler.HandleAsync(command, CancellationToken.None);

        Assert.True(result.IsFailure);
    }
}
