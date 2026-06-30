using GymPlatform.Modules.Training.Domain.Enums;
using GymPlatform.Modules.Training.Domain.Events;
using GymPlatform.Modules.Training.Domain.Exceptions;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Domain.Entities;

public sealed class WorkoutProgram : BaseEntity
{
    private const int MaxNameLength = 200;
    private const int MaxDescriptionLength = 2000;

    private readonly List<ProgramExercise> _exercises = new();
    private readonly List<string> _tags = new();

    private WorkoutProgram()
    {
    }

    public WorkoutProgram(string name, ExerciseCategory category, DifficultyLevel difficulty, Guid coachId, int durationWeeks)
    {
        SetName(name);
        SetCategory(category);
        SetDifficulty(difficulty);
        SetCoachId(coachId);
        SetDurationWeeks(durationWeeks);

        CreatedAt = DateTimeOffset.UtcNow;
        Version = 1;
        AddDomainEvent(new WorkoutProgramCreated(Id, coachId, name));
    }

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public ExerciseCategory Category { get; private set; }

    public DifficultyLevel Difficulty { get; private set; }

    public Guid CoachId { get; private set; }

    public int DurationWeeks { get; private set; }

    public IReadOnlyCollection<ProgramExercise> Exercises => _exercises.AsReadOnly();

    public IReadOnlyCollection<string> Tags => _tags.AsReadOnly();

    public int ExerciseCount => _exercises.Count;

    public int Version { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    public bool IsActive { get; private set; } = true;

    public bool IsPublished { get; private set; }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new TrainingDomainException("Workout program name is required.");
        }

        var trimmed = name.Trim();

        if (trimmed.Length > MaxNameLength)
        {
            throw new TrainingDomainException($"Workout program name cannot exceed {MaxNameLength} characters.");
        }

        Name = trimmed;
    }

    public void SetDescription(string? description)
    {
        Description = string.IsNullOrWhiteSpace(description)
            ? null
            : description.Trim().Length <= MaxDescriptionLength
                ? description.Trim()
                : throw new TrainingDomainException($"Workout program description cannot exceed {MaxDescriptionLength} characters.");
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

    public void SetDurationWeeks(int durationWeeks)
    {
        if (durationWeeks < 1 || durationWeeks > 52)
        {
            throw new TrainingDomainException("Duration must be between 1 and 52 weeks.");
        }

        DurationWeeks = durationWeeks;
    }

    public void AddExercise(Guid exerciseId, int sets, int reps, int order)
    {
        if (exerciseId == Guid.Empty)
        {
            throw new TrainingDomainException("Exercise identifier is required.");
        }

        if (sets < 1)
        {
            throw new TrainingDomainException("Sets must be at least 1.");
        }

        if (reps < 1)
        {
            throw new TrainingDomainException("Reps must be at least 1.");
        }

        if (_exercises.Any(e => e.Order == order))
        {
            throw new TrainingDomainException($"Exercise at order {order} already exists.");
        }

        var programExercise = new ProgramExercise(exerciseId, sets, reps, order);
        _exercises.Add(programExercise);
    }

    public void RemoveExercise(Guid exerciseId)
    {
        var exerciseToRemove = _exercises.FirstOrDefault(e => e.ExerciseId == exerciseId);
        if (exerciseToRemove != null)
        {
            _exercises.Remove(exerciseToRemove);
        }
    }

    public void AddTag(string tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
        {
            throw new TrainingDomainException("Tag is required.");
        }

        var trimmed = tag.Trim().ToLowerInvariant();

        if (!_tags.Contains(trimmed) && _tags.Count < 10)
        {
            _tags.Add(trimmed);
        }
    }

    public void RemoveTag(string tag)
    {
        _tags.Remove(tag.Trim().ToLowerInvariant());
    }

    public void Publish()
    {
        if (_exercises.Count == 0)
        {
            throw new TrainingDomainException("Workout program must have at least one exercise before publishing.");
        }

        IsPublished = true;
    }

    public void Unpublish()
    {
        IsPublished = false;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public WorkoutProgram CreateNewVersion()
    {
        var newVersion = new WorkoutProgram(Name, Category, Difficulty, CoachId, DurationWeeks)
        {
            Description = Description
        };

        foreach (var tag in _tags)
        {
            newVersion._tags.Add(tag);
        }

        foreach (var exercise in _exercises.OrderBy(e => e.Order))
        {
            newVersion.AddExercise(exercise.ExerciseId, exercise.Sets, exercise.Reps, exercise.Order);
        }

        return newVersion;
    }
}