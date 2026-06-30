using System;

namespace GymPlatform.SharedKernel;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
    DateTimeOffset OffsetUtcNow { get; }
}

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTimeOffset OffsetUtcNow => DateTimeOffset.UtcNow;
}