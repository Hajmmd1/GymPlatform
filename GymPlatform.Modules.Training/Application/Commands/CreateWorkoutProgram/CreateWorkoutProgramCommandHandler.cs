using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Repositories;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.CreateWorkoutProgram;

public sealed class CreateWorkoutProgramCommandHandler : ICommandHandler<CreateWorkoutProgramCommand, WorkoutProgramResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWorkoutProgramRepository _programRepository;
    private readonly ICommandValidator<CreateWorkoutProgramCommand> _validator;

    public CreateWorkoutProgramCommandHandler(
        IUnitOfWork unitOfWork,
        IWorkoutProgramRepository programRepository,
        ICommandValidator<CreateWorkoutProgramCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _programRepository = programRepository;
        _validator = validator;
    }

    public async Task<Result<WorkoutProgramResponse>> HandleAsync(CreateWorkoutProgramCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
        {
            return Result<WorkoutProgramResponse>.Failure(validationResult.Error);
        }

        var program = new WorkoutProgram(command.Name, command.Category, command.Difficulty, command.CoachId, command.DurationWeeks);
        program.SetDescription(command.Description);

        if (command.Tags != null)
        {
            foreach (var tag in command.Tags)
            {
                program.AddTag(tag);
            }
        }

        await _programRepository.AddAsync(program, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<WorkoutProgramResponse>.Success(new WorkoutProgramResponse(
            program.Id,
            program.Name,
            program.Description,
            program.Category,
            program.Difficulty,
            program.CoachId,
            program.DurationWeeks,
            program.ExerciseCount,
            program.Version,
            program.CreatedAt,
            program.IsActive,
            program.IsPublished));
    }
}