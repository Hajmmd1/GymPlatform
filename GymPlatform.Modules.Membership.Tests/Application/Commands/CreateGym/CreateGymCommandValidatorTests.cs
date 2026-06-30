using Xunit;
using GymPlatform.Modules.Membership.Application.Commands.CreateGym;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Tests.Application.Commands.CreateGym;

public class CreateGymCommandValidatorTests
{
    [Fact]
    public void Validate_Null_Command_Returns_Failure()
    {
        var validator = new CreateGymCommandValidator();
        var result = validator.Validate(null!);
        Assert.True(result.IsFailure);
        Assert.Equal("Command cannot be null.", result.Error);
    }

    [Fact]
    public void Validate_Empty_Name_Returns_Failure()
    {
        var validator = new CreateGymCommandValidator();
        var command = new CreateGymCommand(string.Empty, null);
        var result = validator.Validate(command);
        Assert.True(result.IsFailure);
        Assert.Equal("Gym name is required.", result.Error);
    }

    [Fact]
    public void Validate_Null_Name_Returns_Failure()
    {
        var validator = new CreateGymCommandValidator();
        var command = new CreateGymCommand(null!, null);
        var result = validator.Validate(command);
        Assert.True(result.IsFailure);
        Assert.Equal("Gym name is required.", result.Error);
    }

    [Fact]
    public void Validate_Valid_Command_Returns_Success()
    {
        var validator = new CreateGymCommandValidator();
        var command = new CreateGymCommand("Iron Fitness", "A great gym");
        var result = validator.Validate(command);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Validate_Valid_Command_Without_Description_Returns_Success()
    {
        var validator = new CreateGymCommandValidator();
        var command = new CreateGymCommand("Iron Fitness", null);
        var result = validator.Validate(command);
        Assert.True(result.IsSuccess);
    }
}