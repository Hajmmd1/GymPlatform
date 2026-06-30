namespace GymPlatform.Modules.Training.Domain.Exceptions;

public sealed class TrainingDomainException : Exception
{
    public TrainingDomainException(string message) : base(message)
    {
    }
}