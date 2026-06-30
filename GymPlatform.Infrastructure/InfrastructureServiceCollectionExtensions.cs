using GymPlatform.Infrastructure.Persistence;
using GymPlatform.Infrastructure.Persistence.Repositories;
using GymPlatform.Modules.Membership.Domain.Repositories;
using GymPlatform.Modules.Training.Domain.Repositories;
using GymPlatform.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymPlatform.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("SQL Server connection string 'Default' is required.");
        }

        services.AddDbContext<MembershipDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddDbContext<TrainingDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IGymRepository, GymRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ICoachRepository, CoachRepository>();

        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IWorkoutProgramRepository, WorkoutProgramRepository>();
        services.AddScoped<IWorkoutLogRepository, WorkoutLogRepository>();
        services.AddScoped<IExerciseVideoRepository, ExerciseVideoRepository>();
        services.AddScoped<IBodyMeasurementRepository, BodyMeasurementRepository>();
        services.AddScoped<IProgressPhotoRepository, ProgressPhotoRepository>();
        services.AddScoped<ICoachProfileRepository, CoachProfileRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MembershipDbContext>());

        return services;
    }
}