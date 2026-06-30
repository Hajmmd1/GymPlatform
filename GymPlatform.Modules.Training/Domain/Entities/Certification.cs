namespace GymPlatform.Modules.Training.Domain.Entities;

public sealed class Certification
{
    public Certification(string name, string? issuingOrganization = null, DateTime? expiresAt = null)
    {
        Name = name;
        IssuingOrganization = issuingOrganization?.Trim();
        ExpiresAt = expiresAt;
        ObtainedAt = DateTime.UtcNow;
    }

    public string Name { get; private set; }

    public string? IssuingOrganization { get; private set; }

    public DateTime? ExpiresAt { get; private set; }

    public DateTime ObtainedAt { get; private set; }

    public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;

    public bool IsVerified { get; private set; }

    public void Verify()
    {
        IsVerified = true;
    }

    public void Unverify()
    {
        IsVerified = false;
    }

    public void UpdateExpiration(DateTime? expiresAt)
    {
        ExpiresAt = expiresAt;
    }
}