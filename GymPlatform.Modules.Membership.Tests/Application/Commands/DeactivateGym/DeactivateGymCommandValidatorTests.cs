using Xunit;
using GymPlatform.Modules.Membership.Application.Commands.DeactivateGym;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Tests.Application.Commands.DeactivateGym;

public class DeactivateGymCommandValidatorTests
{
    [Fact]
    public void Validate_Null_Command_Returns_Failure()
    {
        var validator = new DeactivateGymCommandValidator();
        var result = validator.Validate(null!);
        Assert.True(result.IsFailure);
        Assert.Equal("Command cannot be null.", result.Error);
    }

    [Fact]
    public void Validate_Empty_GymId_Returns_Failure()
    {
        var validator = new DeactivateGymCommandValidator();
        var command = new DeactivateGymCommand(Guid.Empty);
        var result = validator.Validate(command);
        Assert.True(result.IsFailure);
        Assert.Equal("Gym identifier is required.", result.Error);
    }

    [Fact]
    public void Validate_Valid_Command_Returns_Success()
    {
        var validator = new DeactivateGymCommandValidator();
        var command = new DeactivateGymCommand(Guid.NewGuid());
        var result = validator.Validate(command);
        Assert.True(result.IsSuccess);
    }
}