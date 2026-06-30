using GymPlatform.Modules.Membership.Application.Interfaces;
using System.Text.RegularExpressions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Membership.Application.Commands.RegisterMember;

public sealed class RegisterMemberCommandValidator : ICommandValidator<RegisterMemberCommand>
{
    private const string EmailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
    private const string PhonePattern = @"^\+?[1-9]\d{1,14}$";

    public Result Validate(RegisterMemberCommand command)
    {
        if (command is null)
        {
            return Result.Failure("Command cannot be null.");
        }

        if (command.GymId == Guid.Empty)
        {
            return Result.Failure("Gym identifier is required.");
        }

        if (string.IsNullOrWhiteSpace(command.FullName))
        {
            return Result.Failure("Member full name is required.");
        }

        if (string.IsNullOrWhiteSpace(command.Email))
        {
            return Result.Failure("Member email is required.");
        }

        if (!IsValidEmailFormat(command.Email))
        {
            return Result.Failure("Member email format is not valid.");
        }

        if (!string.IsNullOrWhiteSpace(command.Phone) && !IsValidPhoneFormat(command.Phone))
        {
            return Result.Failure("Member phone format is not valid. Use international format (e.g., +1234567890).");
        }

        return Result.Success();
    }

    private static bool IsValidEmailFormat(string email)
    {
        return Regex.IsMatch(email, EmailPattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
    }

    private static bool IsValidPhoneFormat(string phone)
    {
        return Regex.IsMatch(phone, PhonePattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
    }
}