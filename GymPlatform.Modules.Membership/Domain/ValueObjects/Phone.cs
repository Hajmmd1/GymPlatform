using GymPlatform.Modules.Membership.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace GymPlatform.Modules.Membership.Domain.ValueObjects;

public sealed class Phone
{
    private const int MaxLength = 32;
    private const string PhonePattern = @"^\+?[1-9]\d{1,14}$";

    private Phone()
    {
    }

    public Phone(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new MembershipDomainException("Phone is required.");
        }

        var normalized = value.Trim();

        if (normalized.Length > MaxLength)
        {
            throw new MembershipDomainException($"Phone cannot exceed {MaxLength} characters.");
        }

        if (!IsValidPhoneFormat(normalized))
        {
            throw new MembershipDomainException("Phone format is not valid. Use international format (e.g., +1234567890).");
        }

        Value = normalized;
    }

    public string Value { get; private set; } = string.Empty;

    public override string ToString()
    {
        return Value;
    }

    private static bool IsValidPhoneFormat(string phone)
    {
        return Regex.IsMatch(phone, PhonePattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
    }
}