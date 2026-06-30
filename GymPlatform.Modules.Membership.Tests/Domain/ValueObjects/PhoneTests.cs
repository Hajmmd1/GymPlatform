using Xunit;
using GymPlatform.Modules.Membership.Domain.Exceptions;
using GymPlatform.Modules.Membership.Domain.ValueObjects;

namespace GymPlatform.Modules.Membership.Tests.Domain.ValueObjects;

public class PhoneTests
{
    [Fact]
    public void Create_Phone_With_Valid_Value()
    {
        var phone = new Phone("+1234567890");
        Assert.Equal("+1234567890", phone.Value);
    }

    [Fact]
    public void Create_Phone_Without_Plus_With_Valid_Value()
    {
        var phone = new Phone("1234567890");
        Assert.Equal("1234567890", phone.Value);
    }

    [Fact]
    public void Create_Phone_With_Null_Throws_Exception()
    {
        var act = () => new Phone(null!);
        var ex = Assert.Throws<MembershipDomainException>(act);
        Assert.Equal("Phone is required.", ex.Message);
    }

    [Fact]
    public void Create_Phone_With_Empty_Throws_Exception()
    {
        var act = () => new Phone(string.Empty);
        var ex = Assert.Throws<MembershipDomainException>(act);
        Assert.Equal("Phone is required.", ex.Message);
    }

    [Fact]
    public void Create_Phone_With_Too_Long_Value_Throws_Exception()
    {
        var longPhone = new string('1', 33);
        var act = () => new Phone(longPhone);
        var ex = Assert.Throws<MembershipDomainException>(act);
        Assert.Contains("32", ex.Message);
    }

    [Fact]
    public void Create_Phone_With_Invalid_Format_Throws_Exception()
    {
        var act = () => new Phone("invalid-phone");
        var ex = Assert.Throws<MembershipDomainException>(act);
        Assert.Contains("format", ex.Message.ToLower());
    }

    [Fact]
    public void Create_Phone_With_Letters_Throws_Exception()
    {
        var act = () => new Phone("+123456789a");
        var ex = Assert.Throws<MembershipDomainException>(act);
        Assert.Contains("format", ex.Message.ToLower());
    }

    [Fact]
    public void Create_Phone_Trims_Whitespace()
    {
        var phone = new Phone("  +1234567890  ");
        Assert.Equal("+1234567890", phone.Value);
    }
}