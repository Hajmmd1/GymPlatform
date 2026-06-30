using Xunit;
using GymPlatform.Modules.Membership.Domain.Exceptions;
using GymPlatform.Modules.Membership.Domain.ValueObjects;

namespace GymPlatform.Modules.Membership.Tests.Domain.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Create_Email_With_Valid_Value()
    {
        var email = new Email("test@example.com");
        Assert.Equal("test@example.com", email.Value);
    }

    [Fact]
    public void Create_Email_With_Null_Throws_Exception()
    {
        var act = () => new Email(null!);
        var ex = Assert.Throws<MembershipDomainException>(act);
        Assert.Equal("Email is required.", ex.Message);
    }

    [Fact]
    public void Create_Email_With_Empty_Throws_Exception()
    {
        var act = () => new Email(string.Empty);
        var ex = Assert.Throws<MembershipDomainException>(act);
        Assert.Equal("Email is required.", ex.Message);
    }

    [Fact]
    public void Create_Email_With_Whitespace_Throws_Exception()
    {
        var act = () => new Email("   ");
        var ex = Assert.Throws<MembershipDomainException>(act);
        Assert.Equal("Email is required.", ex.Message);
    }

    [Fact]
    public void Create_Email_With_Too_Long_Value_Throws_Exception()
    {
        var longEmail = new string('a', 317) + "@example.com";
        var act = () => new Email(longEmail);
        var ex = Assert.Throws<MembershipDomainException>(act);
        Assert.Contains("320", ex.Message);
    }

    [Fact]
    public void Create_Email_With_Invalid_Format_Throws_Exception()
    {
        var act = () => new Email("invalid-email");
        var ex = Assert.Throws<MembershipDomainException>(act);
        Assert.Contains("format", ex.Message.ToLower());
    }

    [Fact]
    public void Create_Email_With_Missing_At_Symbol_Throws_Exception()
    {
        var act = () => new Email("testexample.com");
        var ex = Assert.Throws<MembershipDomainException>(act);
        Assert.Contains("format", ex.Message.ToLower());
    }

    [Fact]
    public void Create_Email_With_Missing_Domain_Throws_Exception()
    {
        var act = () => new Email("test@");
        var ex = Assert.Throws<MembershipDomainException>(act);
        Assert.Contains("format", ex.Message.ToLower());
    }

    [Fact]
    public void Create_Email_Normalize_To_Lowercase()
    {
        var email = new Email("TEST@EXAMPLE.COM");
        Assert.Equal("test@example.com", email.Value);
    }

    [Fact]
    public void Create_Email_Trims_Whitespace()
    {
        var email = new Email("  test@example.com  ");
        Assert.Equal("test@example.com", email.Value);
    }
}