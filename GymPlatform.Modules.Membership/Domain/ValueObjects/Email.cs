using GymPlatform.Modules.Membership.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace GymPlatform.Modules.Membership.Domain.ValueObjects;

public sealed class Email
{
    private const int MaxLength = 320;
    private const string EmailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

    private Email()
    {
    }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new MembershipDomainException("Email is required.");
        }

        var normalized = value.Trim().ToLowerInvariant();

        if (normalized.Length > MaxLength)
        {
            throw new MembershipDomainException($"Email cannot exceed {MaxLength} characters.");
        }

        if (!IsValidEmailFormat(normalized))
        {
            throw new MembershipDomainException("Email format is not valid.");
        }

        Value = normalized;
    }

    public string Value { get; private set; } = string.Empty;

    public override string ToString()
    {
        return Value;
    }

    private static bool IsValidEmailFormat(string email)
    {
        return Regex.IsMatch(email, EmailPattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
    }
}
