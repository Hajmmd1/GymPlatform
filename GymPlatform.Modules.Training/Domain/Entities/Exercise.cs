using GymPlatform.Modules.Training.Domain.Enums;
using GymPlatform.Modules.Training.Domain.Events;
using GymPlatform.Modules.Training.Domain.Exceptions;
using GymPlatform.Modules.Training.Domain.ValueObjects;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Entities;

public sealed class Exercise : BaseEntity
{
    private const int MaxNameLength = 200;
    private const int MaxDescriptionLength = 1000;

    private readonly List<MuscleGroup> _primaryMuscleGroups = new();
    private readonly List<MuscleGroup> _secondaryMuscleGroups = new();

    private Exercise()
    {
    }

    public Exercise(string name, ExerciseCategory category, DifficultyLevel difficulty, Guid coachId)
    {
        SetName(name);
        SetCategory(category);
        SetDifficulty(difficulty);
        SetCoachId(coachId);

        CreatedAt = DateTimeOffset.UtcNow;
        AddDomainEvent(new ExerciseCreated(Id, coachId, name));
    }

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public ExerciseCategory Category { get; private set; }

    public DifficultyLevel Difficulty { get; private set; }

    public IReadOnlyCollection<MuscleGroup> PrimaryMuscleGroups => _primaryMuscleGroups.AsReadOnly();

    public IReadOnlyCollection<MuscleGroup> SecondaryMuscleGroups => _secondaryMuscleGroups.AsReadOnly();

    public Equipment? RequiredEquipment { get; private set; }

    public Guid CoachId { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public bool IsActive { get; private set; } = true;

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new TrainingDomainException("Exercise name is required.");
        }

        var trimmed = name.Trim();

        if (trimmed.Length > MaxNameLength)
        {
            throw new TrainingDomainException($"Exercise name cannot exceed {MaxNameLength} characters.");
        }

        Name = trimmed;
    }

    public void SetDescription(string? description)
    {
        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim().Length <= MaxDescriptionLength
                ? description.Trim()
                : throw new TrainingDomainException($"Exercise description cannot exceed {MaxDescriptionLength} characters.");
    }

    public void SetCategory(ExerciseCategory category)
    {
        Category = category;
    }

    public void SetDifficulty(DifficultyLevel difficulty)
    {
        Difficulty = difficulty;
    }

    public void SetCoachId(Guid coachId)
    {
        if (coachId == Guid.Empty)
        {
            throw new TrainingDomainException("Coach identifier is required.");
        }

        CoachId = coachId;
    }

    public void SetRequiredEquipment(Equipment? equipment)
    {
        RequiredEquipment = equipment;
    }

    public void AddPrimaryMuscleGroup(MuscleGroup muscleGroup)
    {
        if (!_primaryMuscleGroups.Contains(muscleGroup))
        {
            _primaryMuscleGroups.Add(muscleGroup);
        }
    }

    public void AddSecondaryMuscleGroup(MuscleGroup muscleGroup)
    {
        if (!_secondaryMuscleGroups.Contains(muscleGroup))
        {
            _secondaryMuscleGroups.Add(muscleGroup);
        }
    }

    public void RemovePrimaryMuscleGroup(MuscleGroup muscleGroup)
    {
        _primaryMuscleGroups.Remove(muscleGroup);
    }

    public void RemoveSecondaryMuscleGroup(MuscleGroup muscleGroup)
    {
        _secondaryMuscleGroups.Remove(muscleGroup);
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void EnsureCanBeDeleted()
    {
        if (!IsActive)
        {
            throw new TrainingDomainException("Exercise is already inactive.");
        }
    }

    public void EnsureActive()
    {
        if (!IsActive)
        {
            throw new TrainingDomainException("Exercise must be active.");
        }
    }
}