using Xunit;
using GymPlatform.Modules.Membership.Application.Commands.RegisterMember;
using GymPlatform.Modules.Membership.Application.DTOs;
using GymPlatform.Modules.Membership.Domain.Entities;
using GymPlatform.Modules.Membership.Domain.Enums;
using GymPlatform.Modules.Membership.Domain.Repositories;
using GymPlatform.SharedKernel;
using Moq;

namespace GymPlatform.Modules.Membership.Tests.Application.Commands.RegisterMember;

public class RegisterMemberCommandHandlerTests
{
    [Fact]
    public async Task Handle_Valid_Command_Creates_Member()
    {
        var gymId = Guid.NewGuid();
        var gym = new Gym("Iron Fitness");

        var gymRepositoryMock = new Mock<IGymRepository>();
        var memberRepositoryMock = new Mock<IMemberRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        gymRepositoryMock
            .Setup(r => r.GetByIdAsync(gymId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(gym);

        memberRepositoryMock
            .Setup(r => r.GetByEmailAndGymIdAsync("john@example.com", gymId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Member)null!);

        memberRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Member>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1))
            .Verifiable();

        var handler = new RegisterMemberCommandHandler(
            unitOfWorkMock.Object,
            gymRepositoryMock.Object,
            memberRepositoryMock.Object);

        var command = new RegisterMemberCommand(gymId, "John Doe", "john@example.com", "+1234567890", MemberStatus.Active);
        var result = await handler.HandleAsync(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("John Doe", result.Value!.FullName);
        Assert.Equal("john@example.com", result.Value.Email);
        Assert.Equal("+1234567890", result.Value.Phone);

        memberRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Member>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Invalid_Command_Returns_Failure()
    {
        var gymRepositoryMock = new Mock<IGymRepository>();
        var memberRepositoryMock = new Mock<IMemberRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var handler = new RegisterMemberCommandHandler(
            unitOfWorkMock.Object,
            gymRepositoryMock.Object,
            memberRepositoryMock.Object);

        var command = new RegisterMemberCommand(Guid.Empty, "John Doe", "john@example.com", "+1234567890", MemberStatus.Active);
        var result = await handler.HandleAsync(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("Gym not found.", result.Error);

        memberRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Member>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}