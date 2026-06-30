using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.Modules.Training.Application.Interfaces;
using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Repositories;
using GymPlatform.SharedKernel;

namespace GymPlatform.Modules.Training.Application.Commands.LogWorkout;

public sealed class LogWorkoutCommandHandler : ICommandHandler<LogWorkoutCommand, WorkoutLogResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWorkoutLogRepository _workoutLogRepository;
    private readonly ICommandValidator<LogWorkoutCommand> _validator;

    public LogWorkoutCommandHandler(
        IUnitOfWork unitOfWork,
        IWorkoutLogRepository workoutLogRepository,
        ICommandValidator<LogWorkoutCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _workoutLogRepository = workoutLogRepository;
        _validator = validator;
    }

    public async Task<Result<WorkoutLogResponse>> HandleAsync(LogWorkoutCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsFailure)
        {
            return Result<WorkoutLogResponse>.Failure(validationResult.Error);
        }

        var workoutLog = new WorkoutLog(command.MemberId, command.ProgramId);
        
        if (command.ExerciseId.HasValue)
        {
            workoutLog.SetExerciseId(command.ExerciseId.Value);
        }

        if (command.SetsCompleted.HasValue || command.RepsCompleted.HasValue)
        {
            workoutLog.Complete(
                command.SetsCompleted ?? 0,
                command.RepsCompleted ?? 0,
                command.WeightUsed,
                command.Duration);
        }

        workoutLog.SetNotes(command.Notes);

        await _workoutLogRepository.AddAsync(workoutLog, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<WorkoutLogResponse>.Success(new WorkoutLogResponse(
            workoutLog.Id,
            workoutLog.MemberId,
            workoutLog.ProgramId,
            workoutLog.ExerciseId,
            workoutLog.SetsCompleted,
            workoutLog.RepsCompleted,
            workoutLog.WeightUsed,
            workoutLog.Duration,
            workoutLog.LoggedAt,
            workoutLog.CompletedAt,
            workoutLog.Notes,
            workoutLog.IsCompleted));
    }
}