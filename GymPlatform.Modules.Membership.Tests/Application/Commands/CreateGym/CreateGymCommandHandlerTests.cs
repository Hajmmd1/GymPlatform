using Xunit;
using GymPlatform.Modules.Membership.Application.Commands.CreateGym;
using GymPlatform.Modules.Membership.Application.DTOs;
using GymPlatform.Modules.Membership.Domain.Entities;
using GymPlatform.Modules.Membership.Domain.Repositories;
using GymPlatform.SharedKernel;
using Moq;

namespace GymPlatform.Modules.Membership.Tests.Application.Commands.CreateGym;

public class CreateGymCommandHandlerTests
{
    [Fact]
    public async Task Handle_Valid_Command_Creates_Gym()
    {
        var gymRepositoryMock = new Mock<IGymRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        gymRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Gym>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1))
            .Verifiable();

        var handler = new CreateGymCommandHandler(
            unitOfWorkMock.Object,
            gymRepositoryMock.Object,
            new CreateGymCommandValidator());

        var command = new CreateGymCommand("Iron Fitness", "A great gym");
        var result = await handler.HandleAsync(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("Iron Fitness", result.Value!.Name);
        Assert.Equal("A great gym", result.Value.Description);
        Assert.True(result.Value.IsActive);

        gymRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Gym>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Invalid_Command_Returns_Failure()
    {
        var gymRepositoryMock = new Mock<IGymRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var handler = new CreateGymCommandHandler(
            unitOfWorkMock.Object,
            gymRepositoryMock.Object,
            new CreateGymCommandValidator());

        var command = new CreateGymCommand(string.Empty, null);
        var result = await handler.HandleAsync(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Contains("Gym name is required", result.Error);

        gymRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Gym>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}