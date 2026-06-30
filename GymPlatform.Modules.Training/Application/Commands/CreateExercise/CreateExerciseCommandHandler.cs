using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Enums;
using GymPlatform.Modules.Training.Domain.Repositories;
using GymPlatform.Modules.Training.Domain.ValueObjects;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.CreateExercise;

public sealed class CreateExerciseCommandHandler : ICommandHandler<CreateExerciseCommand, ExerciseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ICommandValidator<CreateExerciseCommand> _validator;

    public CreateExerciseCommandHandler(
        IUnitOfWork unitOfWork,
        IExerciseRepository exerciseRepository,
        ICommandValidator<CreateExerciseCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _exerciseRepository = exerciseRepository;
        _validator = validator;
    }

    public async Task<Result<ExerciseResponse>> HandleAsync(CreateExerciseCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
        {
            return Result<ExerciseResponse>.Failure(validationResult.Error);
        }

        var existingExercise = await _exerciseRepository.GetByNameAsync(command.Name, cancellationToken);
        if (existingExercise != null)
        {
            return Result<ExerciseResponse>.Failure("An exercise with this name already exists.");
        }

        Equipment? equipment = null;
        if (!string.IsNullOrWhiteSpace(command.EquipmentName))
        {
            equipment = new Equipment(command.EquipmentName);
        }

        var exercise = new Exercise(command.Name, command.Category, command.Difficulty, command.CoachId);
        exercise.SetDescription(command.Description);
        exercise.SetRequiredEquipment(equipment);

        await _exerciseRepository.AddAsync(exercise, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ExerciseResponse>.Success(new ExerciseResponse(
            exercise.Id,
            exercise.Name,
            exercise.Description,
            exercise.Category,
            exercise.Difficulty,
            exercise.RequiredEquipment?.Value,
            exercise.CoachId,
            exercise.CreatedAt,
            exercise.IsActive));
    }
}