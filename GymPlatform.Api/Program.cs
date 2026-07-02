using GymPlatform.Infrastructure;
using GymPlatform.Modules.Membership.Application.Commands.AssignMemberToCoach;
using GymPlatform.Modules.Membership.Application.Commands.CreateGym;
using GymPlatform.Modules.Membership.Application.Commands.DeactivateGym;
using GymPlatform.Modules.Membership.Application.Commands.RegisterMember;
using GymPlatform.Modules.Membership.Application.DTOs;
using GymPlatform.Modules.Membership.Application.Interfaces;
using GymPlatform.Modules.Training.Application.Commands.CreateExercise;
using GymPlatform.Modules.Training.Application.Commands.CreateWorkoutProgram;
using GymPlatform.Modules.Training.Application.Commands.LogWorkout;
using GymPlatform.Modules.Training.Application.Commands.RecordBodyMeasurement;
using GymPlatform.Modules.Training.Application.Commands.UploadExerciseVideo;
using GymPlatform.Modules.Training.Application.Commands.UploadProgressPhoto;
using GymPlatform.Modules.Training.Application.Commands.UpdateCoachProfile;
using GymPlatform.Modules.Training.Application.DTOs;
using GymPlatform.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace GymPlatform.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHealthChecks();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<DateTimeProvider>();
        builder.Services.AddScoped<CurrentUserService>();

        // Membership command handlers
        builder.Services.AddScoped<ICommandHandler<CreateGymCommand, GymResponse>, CreateGymCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<RegisterMemberCommand, MemberResponse>, RegisterMemberCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<AssignMemberToCoachCommand, MemberCoachAssignmentResponse>, AssignMemberToCoachCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<DeactivateGymCommand, GymResponse>, DeactivateGymCommandHandler>();

        // Training command handlers
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<CreateExerciseCommand, ExerciseResponse>, CreateExerciseCommandHandler>();
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<CreateWorkoutProgramCommand, WorkoutProgramResponse>, CreateWorkoutProgramCommandHandler>();
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<LogWorkoutCommand, WorkoutLogResponse>, LogWorkoutCommandHandler>();
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<UploadExerciseVideoCommand, ExerciseVideoResponse>, UploadExerciseVideoCommandHandler>();
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<RecordBodyMeasurementCommand, BodyMeasurementResponse>, RecordBodyMeasurementCommandHandler>();
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<UploadProgressPhotoCommand, ProgressPhotoResponse>, UploadProgressPhotoCommandHandler>();
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<UpdateCoachProfileCommand, CoachProfileResponse>, UpdateCoachProfileCommandHandler>();

        // Training validators
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandValidator<CreateExerciseCommand>, CreateExerciseCommandValidator>();
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandValidator<CreateWorkoutProgramCommand>, CreateWorkoutProgramCommandValidator>();
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandValidator<LogWorkoutCommand>, LogWorkoutCommandValidator>();
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandValidator<UploadExerciseVideoCommand>, UploadExerciseVideoCommandValidator>();
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandValidator<RecordBodyMeasurementCommand>, RecordBodyMeasurementCommandValidator>();
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandValidator<UploadProgressPhotoCommand>, UploadProgressPhotoCommandValidator>();
        builder.Services.AddScoped<GymPlatform.Modules.Training.Application.Interfaces.ICommandValidator<UpdateCoachProfileCommand>, UpdateCoachProfileCommandValidator>();

        // Communication command handlers
        builder.Services.AddScoped<GymPlatform.Modules.Communication.Application.Interfaces.ICommandHandler<GymPlatform.Modules.Communication.Application.Commands.CreateRoom.CreateRoomCommand, GymPlatform.Modules.Communication.Application.DTOs.RoomResponse>, GymPlatform.Modules.Communication.Application.Commands.CreateRoom.CreateRoomCommandHandler>();
        builder.Services.AddScoped<GymPlatform.Modules.Communication.Application.Interfaces.ICommandHandler<GymPlatform.Modules.Communication.Application.Commands.CreateSession.CreateSessionCommand, GymPlatform.Modules.Communication.Application.DTOs.SessionResponse>, GymPlatform.Modules.Communication.Application.Commands.CreateSession.CreateSessionCommandHandler>();
        builder.Services.AddScoped<GymPlatform.Modules.Communication.Application.Interfaces.ICommandHandler<GymPlatform.Modules.Communication.Application.Commands.BookSession.BookSessionCommand, GymPlatform.Modules.Communication.Application.DTOs.BookingResponse>, GymPlatform.Modules.Communication.Application.Commands.BookSession.BookSessionCommandHandler>();
        builder.Services.AddScoped<GymPlatform.Modules.Communication.Application.Interfaces.ICommandHandler<GymPlatform.Modules.Communication.Application.Commands.CancelBooking.CancelBookingCommand, GymPlatform.Modules.Communication.Application.DTOs.BookingResponse>, GymPlatform.Modules.Communication.Application.Commands.CancelBooking.CancelBookingCommandHandler>();
        builder.Services.AddScoped<GymPlatform.Modules.Communication.Application.Interfaces.ICommandHandler<GymPlatform.Modules.Communication.Application.Commands.CancelSession.CancelSessionCommand, GymPlatform.Modules.Communication.Application.DTOs.SessionResponse>, GymPlatform.Modules.Communication.Application.Commands.CancelSession.CancelSessionCommandHandler>();
        builder.Services.AddScoped<GymPlatform.Modules.Communication.Application.Interfaces.ICommandHandler<GymPlatform.Modules.Communication.Application.Commands.SetCoachAvailability.SetCoachAvailabilityCommand, GymPlatform.Modules.Communication.Application.DTOs.CoachAvailabilityResponse>, GymPlatform.Modules.Communication.Application.Commands.SetCoachAvailability.SetCoachAvailabilityCommandHandler>();

        // Communication validators
        builder.Services.AddScoped<GymPlatform.Modules.Communication.Application.Interfaces.ICommandValidator<GymPlatform.Modules.Communication.Application.Commands.CreateRoom.CreateRoomCommand>, GymPlatform.Modules.Communication.Application.Commands.CreateRoom.CreateRoomCommandValidator>();
        builder.Services.AddScoped<GymPlatform.Modules.Communication.Application.Interfaces.ICommandValidator<GymPlatform.Modules.Communication.Application.Commands.CreateSession.CreateSessionCommand>, GymPlatform.Modules.Communication.Application.Commands.CreateSession.CreateSessionCommandValidator>();
        builder.Services.AddScoped<GymPlatform.Modules.Communication.Application.Interfaces.ICommandValidator<GymPlatform.Modules.Communication.Application.Commands.BookSession.BookSessionCommand>, GymPlatform.Modules.Communication.Application.Commands.BookSession.BookSessionCommandValidator>();
        builder.Services.AddScoped<GymPlatform.Modules.Communication.Application.Interfaces.ICommandValidator<GymPlatform.Modules.Communication.Application.Commands.CancelBooking.CancelBookingCommand>, GymPlatform.Modules.Communication.Application.Commands.CancelBooking.CancelBookingCommandValidator>();
        builder.Services.AddScoped<GymPlatform.Modules.Communication.Application.Interfaces.ICommandValidator<GymPlatform.Modules.Communication.Application.Commands.CancelSession.CancelSessionCommand>, GymPlatform.Modules.Communication.Application.Commands.CancelSession.CancelSessionCommandValidator>();
        builder.Services.AddScoped<GymPlatform.Modules.Communication.Application.Interfaces.ICommandValidator<GymPlatform.Modules.Communication.Application.Commands.SetCoachAvailability.SetCoachAvailabilityCommand>, GymPlatform.Modules.Communication.Application.Commands.SetCoachAvailability.SetCoachAvailabilityCommandValidator>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.MapHealthChecks("/health");

        // Membership endpoints
        app.MapPost("/api/gyms", async (
            [FromBody] CreateGymRequest request,
            [FromServices] ICommandHandler<CreateGymCommand, GymResponse> handler) =>
        {
            var command = new CreateGymCommand(request.Name, request.Description);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Created($"/api/gyms/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("CreateGym");

        app.MapPost("/api/members", async (
            [FromBody] RegisterMemberRequest request,
            [FromServices] ICommandHandler<RegisterMemberCommand, MemberResponse> handler) =>
        {
            var command = new RegisterMemberCommand(request.GymId, request.FullName, request.Email, request.Phone, request.Status);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Created($"/api/members/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("RegisterMember");

        app.MapPost("/api/members/{memberId:guid}/assign-coach", async (
            [FromRoute] Guid memberId,
            [FromBody] AssignMemberToCoachRequest request,
            [FromServices] ICommandHandler<AssignMemberToCoachCommand, MemberCoachAssignmentResponse> handler) =>
        {
            var command = new AssignMemberToCoachCommand(memberId, request.CoachId);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("AssignMemberToCoach");

        app.MapDelete("/api/gyms/{gymId:guid}", async (
            [FromRoute] Guid gymId,
            [FromServices] ICommandHandler<DeactivateGymCommand, GymResponse> handler) =>
        {
            var command = new DeactivateGymCommand(gymId);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("DeactivateGym");

        // Training endpoints - Exercises
        app.MapPost("/api/exercises", async (
            [FromBody] CreateExerciseRequest request,
            [FromServices] GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<CreateExerciseCommand, ExerciseResponse> handler) =>
        {
            var command = new CreateExerciseCommand(request.Name, request.Description, request.Category, request.Difficulty, request.CoachId, request.Equipment);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Created($"/api/exercises/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("CreateExercise");

        // Training endpoints - Workout Programs
        app.MapPost("/api/workout-programs", async (
            [FromBody] CreateWorkoutProgramRequest request,
            [FromServices] GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<CreateWorkoutProgramCommand, WorkoutProgramResponse> handler) =>
        {
            var command = new CreateWorkoutProgramCommand(request.Name, request.Description, request.Category, request.Difficulty, request.CoachId, request.DurationWeeks, request.Tags);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Created($"/api/workout-programs/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("CreateWorkoutProgram");

        // Training endpoints - Workout Logs
        app.MapPost("/api/workout-logs", async (
            [FromBody] LogWorkoutRequest request,
            [FromServices] GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<LogWorkoutCommand, WorkoutLogResponse> handler) =>
        {
            var command = new LogWorkoutCommand(request.MemberId, request.ProgramId, request.ExerciseId, request.SetsCompleted, request.RepsCompleted, request.WeightUsed, request.Duration, request.Notes);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Created($"/api/workout-logs/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("LogWorkout");

        // Training endpoints - Exercise Videos
        app.MapPost("/api/exercise-videos", async (
            [FromBody] UploadExerciseVideoRequest request,
            [FromServices] GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<UploadExerciseVideoCommand, ExerciseVideoResponse> handler) =>
        {
            var command = new UploadExerciseVideoCommand(request.ExerciseId, request.Title, request.Description, request.VideoUrl!, request.ThumbnailUrl, request.DurationSeconds, request.CoachId);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Created($"/api/exercise-videos/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("UploadExerciseVideo");

        // Training endpoints - Body Measurements
        app.MapPost("/api/body-measurements", async (
            [FromBody] RecordBodyMeasurementRequest request,
            [FromServices] GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<RecordBodyMeasurementCommand, BodyMeasurementResponse> handler) =>
        {
            var command = new RecordBodyMeasurementCommand(request.MemberId, request.Type, request.Value, request.Unit, request.MeasuredAt, request.Notes);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Created($"/api/body-measurements/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("RecordBodyMeasurement");

        // Training endpoints - Progress Photos
        app.MapPost("/api/progress-photos", async (
            [FromBody] UploadProgressPhotoRequest request,
            [FromServices] GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<UploadProgressPhotoCommand, ProgressPhotoResponse> handler) =>
        {
            var command = new UploadProgressPhotoCommand(request.MemberId, request.PhotoUrl, request.CapturedAt, request.ThumbnailUrl, request.Notes, request.IsPrivate);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Created($"/api/progress-photos/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("UploadProgressPhoto");

        // Training endpoints - Coach Profiles
        app.MapPatch("/api/coach-profiles", async (
            [FromBody] UpdateCoachProfileRequest request,
            [FromServices] GymPlatform.Modules.Training.Application.Interfaces.ICommandHandler<UpdateCoachProfileCommand, CoachProfileResponse> handler) =>
        {
            var command = new UpdateCoachProfileCommand(request.CoachId, request.Bio, request.ProfilePhotoUrl, request.Specialties);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("UpdateCoachProfile");

        // Calendar endpoints - Rooms
        app.MapPost("/api/rooms", async (
            [FromBody] GymPlatform.Modules.Communication.Application.DTOs.CreateRoomRequest request,
            [FromServices] GymPlatform.Modules.Communication.Application.Interfaces.ICommandHandler<GymPlatform.Modules.Communication.Application.Commands.CreateRoom.CreateRoomCommand, GymPlatform.Modules.Communication.Application.DTOs.RoomResponse> handler) =>
        {
            var command = new GymPlatform.Modules.Communication.Application.Commands.CreateRoom.CreateRoomCommand(request.GymId, request.Name, request.Capacity);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Created($"/api/rooms/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("CreateRoom");

        // Calendar endpoints - Sessions
        app.MapPost("/api/sessions", async (
            [FromBody] GymPlatform.Modules.Communication.Application.DTOs.CreateSessionRequest request,
            [FromServices] GymPlatform.Modules.Communication.Application.Interfaces.ICommandHandler<GymPlatform.Modules.Communication.Application.Commands.CreateSession.CreateSessionCommand, GymPlatform.Modules.Communication.Application.DTOs.SessionResponse> handler) =>
        {
            var command = new GymPlatform.Modules.Communication.Application.Commands.CreateSession.CreateSessionCommand(request.GymId, request.CoachId, request.RoomId, request.Name, request.SessionType, request.StartTime, request.EndTime, request.MaxCapacity);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Created($"/api/sessions/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("CreateSession");

        // Calendar endpoints - Bookings
        app.MapPost("/api/bookings", async (
            [FromBody] GymPlatform.Modules.Communication.Application.DTOs.BookSessionRequest request,
            [FromServices] GymPlatform.Modules.Communication.Application.Interfaces.ICommandHandler<GymPlatform.Modules.Communication.Application.Commands.BookSession.BookSessionCommand, GymPlatform.Modules.Communication.Application.DTOs.BookingResponse> handler,
            [FromServices] GymPlatform.SharedKernel.ICurrentUserService currentUserService) =>
        {
            var memberId = currentUserService.UserId ?? Guid.Empty;
            var command = new GymPlatform.Modules.Communication.Application.Commands.BookSession.BookSessionCommand(request.SessionId, memberId);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Created($"/api/bookings/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("BookSession");

        // Calendar endpoints - Cancel Session
        app.MapPost("/api/sessions/{sessionId:guid}/cancel", async (
            [FromRoute] Guid sessionId,
            [FromServices] GymPlatform.Modules.Communication.Application.Interfaces.ICommandHandler<GymPlatform.Modules.Communication.Application.Commands.CancelSession.CancelSessionCommand, GymPlatform.Modules.Communication.Application.DTOs.SessionResponse> handler) =>
        {
            var command = new GymPlatform.Modules.Communication.Application.Commands.CancelSession.CancelSessionCommand(sessionId);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("CancelSession");

        // Calendar endpoints - Cancel Booking
        app.MapPost("/api/bookings/{bookingId:guid}/cancel", async (
            [FromRoute] Guid bookingId,
            [FromServices] GymPlatform.Modules.Communication.Application.Interfaces.ICommandHandler<GymPlatform.Modules.Communication.Application.Commands.CancelBooking.CancelBookingCommand, GymPlatform.Modules.Communication.Application.DTOs.BookingResponse> handler) =>
        {
            var command = new GymPlatform.Modules.Communication.Application.Commands.CancelBooking.CancelBookingCommand(bookingId);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("CancelBooking");

        // Calendar endpoints - Coach Availability
        app.MapPost("/api/coaches/{coachId:guid}/availability", async (
            [FromRoute] Guid coachId,
            [FromBody] GymPlatform.Modules.Communication.Application.DTOs.SetCoachAvailabilityRequest request,
            [FromServices] GymPlatform.Modules.Communication.Application.Interfaces.ICommandHandler<GymPlatform.Modules.Communication.Application.Commands.SetCoachAvailability.SetCoachAvailabilityCommand, GymPlatform.Modules.Communication.Application.DTOs.CoachAvailabilityResponse> handler) =>
        {
            var command = new GymPlatform.Modules.Communication.Application.Commands.SetCoachAvailability.SetCoachAvailabilityCommand(coachId, request.GymId, request.StartTime, request.EndTime, request.IsAvailable);
            var result = await handler.HandleAsync(command);

            return result.IsSuccess
                ? Results.Created($"/api/coaches/{coachId}/availability/{result.Value!.Id}", result.Value)
                : Results.BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Validation Error",
                    Detail = result.Error,
                    Type = "https://httpstatuses.com/400"
                });
        })
        .WithName("SetCoachAvailability");

        app.Run();
    }
}