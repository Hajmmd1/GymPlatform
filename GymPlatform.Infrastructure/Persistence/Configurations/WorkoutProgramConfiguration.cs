using GymPlatform.Modules.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymPlatform.Infrastructure.Persistence.Configurations;

public sealed class WorkoutProgramConfiguration : IEntityTypeConfiguration<WorkoutProgram>
{
    public void Configure(EntityTypeBuilder<WorkoutProgram> builder)
    {
        builder.ToTable("workout_programs", "training");

        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id)
            .HasColumnName("id");

        builder.Property(entity => entity.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(entity => entity.Description)
            .HasColumnName("description")
            .HasMaxLength(2000);

        builder.Property(entity => entity.Category)
            .HasColumnName("category")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(entity => entity.Difficulty)
            .HasColumnName("difficulty")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(entity => entity.CoachId)
            .HasColumnName("coach_id")
            .IsRequired();

        builder.Property(entity => entity.DurationWeeks)
            .HasColumnName("duration_weeks")
            .IsRequired();

        builder.Property(entity => entity.ExerciseCount)
            .HasColumnName("exercise_count")
            .IsRequired();

        builder.Property(entity => entity.Version)
            .HasColumnName("version")
            .IsRequired();

        builder.Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(entity => entity.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(entity => entity.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(entity => entity.IsPublished)
            .HasColumnName("is_published")
            .IsRequired();

        builder.OwnsMany(entity => entity.Exercises, exercise =>
        {
            exercise.ToTable("workout_program_exercises", "training");
            exercise.WithOwner().HasForeignKey("program_id");
            exercise.Property(e => e.ExerciseId).HasColumnName("exercise_id").IsRequired();
            exercise.Property(e => e.Sets).HasColumnName("sets").IsRequired();
            exercise.Property(e => e.Reps).HasColumnName("reps").IsRequired();
            exercise.Property(e => e.Order).HasColumnName("order").IsRequired();
            exercise.Property(e => e.RestSeconds).HasColumnName("rest_seconds");
            exercise.Property(e => e.Notes).HasColumnName("notes").HasMaxLength(500);

            exercise.HasKey("program_id", "order");
        });

        builder.Ignore(entity => entity.Tags);
    }
}