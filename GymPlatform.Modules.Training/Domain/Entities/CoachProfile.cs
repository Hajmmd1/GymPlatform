using GymPlatform.Modules.Training.Domain.Events;
using GymPlatform.Modules.Training.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Entities;

public sealed class CoachProfile : BaseEntity
{
    private const int MaxBioLength = 2000;
    private const int MaxSpecialtyLength = 100;
    private const int MaxCertificationLength = 200;

    private readonly List<string> _specialties = new();
    private readonly List<Certification> _certifications = new();

    private CoachProfile()
    {
    }

    public CoachProfile(Guid coachId)
    {
        SetCoachId(coachId);
        CreatedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new CoachProfileUpdated(coachId, Id));
    }

    public Guid CoachId { get; private set; }

    public string? Bio { get; private set; }

    public string? ProfilePhotoUrl { get; private set; }

    public IReadOnlyCollection<string> Specialties => _specialties.AsReadOnly();

    public IReadOnlyCollection<Certification> Certifications => _certifications.AsReadOnly();

    public decimal? Rating { get; private set; }

    public int RatingCount { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset UpdatedAt { get; private set; }

    public bool IsActive { get; private set; } = true;

    public void SetCoachId(Guid coachId)
    {
        if (coachId == Guid.Empty)
        {
            throw new TrainingDomainException("Coach identifier is required.");
        }

        CoachId = coachId;
    }

    public void SetBio(string? bio)
    {
        if (!string.IsNullOrWhiteSpace(bio) && bio.Trim().Length > MaxBioLength)
        {
            throw new TrainingDomainException($"Bio cannot exceed {MaxBioLength} characters.");
        }

        Bio = bio?.Trim();
    }

    public void SetProfilePhotoUrl(string? profilePhotoUrl)
    {
        ProfilePhotoUrl = profilePhotoUrl?.Trim();
    }

    public void AddSpecialty(string specialty)
    {
        if (string.IsNullOrWhiteSpace(specialty))
        {
            throw new TrainingDomainException("Specialty is required.");
        }

        var trimmed = specialty.Trim();

        if (trimmed.Length > MaxSpecialtyLength)
        {
            throw new TrainingDomainException($"Specialty cannot exceed {MaxSpecialtyLength} characters.");
        }

        if (!_specialties.Contains(trimmed) && _specialties.Count < 20)
        {
            _specialties.Add(trimmed);
        }
    }

    public void RemoveSpecialty(string specialty)
    {
        _specialties.Remove(specialty.Trim());
    }

    public void AddCertification(string name, string? issuingOrganization = null, DateTime? expiresAt = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new TrainingDomainException("Certification name is required.");
        }

        var trimmed = name.Trim();

        if (trimmed.Length > MaxCertificationLength)
        {
            throw new TrainingDomainException($"Certification name cannot exceed {MaxCertificationLength} characters.");
        }

        var certification = new Certification(trimmed, issuingOrganization, expiresAt);
        _certifications.Add(certification);
    }

    public void RemoveCertification(string name)
    {
        var certToRemove = _certifications.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (certToRemove != null)
        {
            _certifications.Remove(certToRemove);
        }
    }

    public void UpdateRating(decimal rating)
    {
        if (rating < 0 || rating > 5)
        {
            throw new TrainingDomainException("Rating must be between 0 and 5.");
        }

        Rating = rating;
        RatingCount++;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}