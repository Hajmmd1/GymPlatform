namespace GymPlatform.Modules.Membership.Domain.Exceptions;

public sealed class MembershipDomainException : Exception
{
    public MembershipDomainException(string message)
        : base(message)
    {
    }
}
