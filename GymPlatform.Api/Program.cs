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

        builder.Services.AddScoped<ICommandHandler<CreateGymCommand, GymResponse>, CreateGymCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<RegisterMemberCommand, MemberResponse>, RegisterMemberCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<AssignMemberToCoachCommand, MemberCoachAssignmentResponse>, AssignMemberToCoachCommandHandler>();
        builder.Services.AddScoped<ICommandHandler<DeactivateGymCommand, GymResponse>, DeactivateGymCommandHandler>();

        builder.Services.AddScoped(typeof(GymPlatform.Modules.Training.Application.Interfaces.ICommandValidator<>), typeof(GymPlatform.Modules.Training.Application.Interfaces.ICommandValidator<>));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.MapHealthChecks("/health");

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

        app.Run();
    }
}