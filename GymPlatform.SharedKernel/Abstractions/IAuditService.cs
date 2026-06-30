namespace GymPlatform.SharedKernel;

public interface IAuditService
{
    Guid? CreatedBy { get; }
    DateTimeOffset CreatedAt { get; }
    Guid? ModifiedBy { get; }
    DateTimeOffset? ModifiedAt { get; }
}