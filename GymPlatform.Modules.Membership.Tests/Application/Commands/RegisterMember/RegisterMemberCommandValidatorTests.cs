using Xunit;
using GymPlatform.Modules.Membership.Application.Commands.RegisterMember;
using GymPlatform.Modules.Membership.Domain.Enums;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Tests.Application.Commands.RegisterMember;

public class RegisterMemberCommandValidatorTests
{
    [Fact]
    public void Validate_Null_Command_Returns_Failure()
    {
        var validator = new RegisterMemberCommandValidator();
        var result = validator.Validate(null!);
        Assert.True(result.IsFailure);
        Assert.Equal("Command cannot be null.", result.Error);
    }

    [Fact]
    public void Validate_Empty_GymId_Returns_Failure()
    {
        var validator = new RegisterMemberCommandValidator();
        var command = new RegisterMemberCommand(Guid.Empty, "John Doe", "john@example.com", null, MemberStatus.Active);
        var result = validator.Validate(command);
        Assert.True(result.IsFailure);
        Assert.Equal("Gym identifier is required.", result.Error);
    }

    [Fact]
    public void Validate_Empty_FullName_Returns_Failure()
    {
        var validator = new RegisterMemberCommandValidator();
        var command = new RegisterMemberCommand(Guid.NewGuid(), string.Empty, "john@example.com", null, MemberStatus.Active);
        var result = validator.Validate(command);
        Assert.True(result.IsFailure);
        Assert.Equal("Member full name is required.", result.Error);
    }

    [Fact]
    public void Validate_Empty_Email_Returns_Failure()
    {
        var validator = new RegisterMemberCommandValidator();
        var command = new RegisterMemberCommand(Guid.NewGuid(), "John Doe", string.Empty, null, MemberStatus.Active);
        var result = validator.Validate(command);
        Assert.True(result.IsFailure);
        Assert.Equal("Member email is required.", result.Error);
    }

    [Fact]
    public void Validate_Invalid_Email_Format_Returns_Failure()
    {
        var validator = new RegisterMemberCommandValidator();
        var command = new RegisterMemberCommand(Guid.NewGuid(), "John Doe", "invalid-email", null, MemberStatus.Active);
        var result = validator.Validate(command);
        Assert.True(result.IsFailure);
        Assert.Contains("email format", result.Error.ToLower());
    }

    [Fact]
    public void Validate_Invalid_Phone_Format_Returns_Failure()
    {
        var validator = new RegisterMemberCommandValidator();
        var command = new RegisterMemberCommand(Guid.NewGuid(), "John Doe", "john@example.com", "invalid-phone", MemberStatus.Active);
        var result = validator.Validate(command);
        Assert.True(result.IsFailure);
        Assert.Contains("phone format", result.Error.ToLower());
    }

    [Fact]
    public void Validate_Valid_Command_Returns_Success()
    {
        var validator = new RegisterMemberCommandValidator();
        var command = new RegisterMemberCommand(Guid.NewGuid(), "John Doe", "john@example.com", "+1234567890", MemberStatus.Active);
        var result = validator.Validate(command);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Validate_Valid_Command_Without_Phone_Returns_Success()
    {
        var validator = new RegisterMemberCommandValidator();
        var command = new RegisterMemberCommand(Guid.NewGuid(), "John Doe", "john@example.com", null, MemberStatus.Active);
        var result = validator.Validate(command);
        Assert.True(result.IsSuccess);
    }
}