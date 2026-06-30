using Xunit;
using GymPlatform.Modules.Membership.Application.Commands.AssignMemberToCoach;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Tests.Application.Commands.AssignMemberToCoach;

public class AssignMemberToCoachCommandValidatorTests
{
    [Fact]
    public void Validate_Null_Command_Returns_Failure()
    {
        var validator = new AssignMemberToCoachCommandValidator();
        var result = validator.Validate(null!);
        Assert.True(result.IsFailure);
        Assert.Equal("Command cannot be null.", result.Error);
    }

    [Fact]
    public void Validate_Empty_MemberId_Returns_Failure()
    {
        var validator = new AssignMemberToCoachCommandValidator();
        var command = new AssignMemberToCoachCommand(Guid.Empty, Guid.NewGuid());
        var result = validator.Validate(command);
        Assert.True(result.IsFailure);
        Assert.Equal("Member identifier is required.", result.Error);
    }

    [Fact]
    public void Validate_Empty_CoachId_Returns_Failure()
    {
        var validator = new AssignMemberToCoachCommandValidator();
        var command = new AssignMemberToCoachCommand(Guid.NewGuid(), Guid.Empty);
        var result = validator.Validate(command);
        Assert.True(result.IsFailure);
        Assert.Equal("Coach identifier is required.", result.Error);
    }

    [Fact]
    public void Validate_Valid_Command_Returns_Success()
    {
        var validator = new AssignMemberToCoachCommandValidator();
        var command = new AssignMemberToCoachCommand(Guid.NewGuid(), Guid.NewGuid());
        var result = validator.Validate(command);
        Assert.True(result.IsSuccess);
    }
}