using GymPlatform.Modules.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymPlatform.Infrastructure.Persistence.Configurations;

public sealed class WorkoutLogConfiguration : IEntityTypeConfiguration<WorkoutLog>
{
    public void Configure(EntityTypeBuilder<WorkoutLog> builder)
    {
        builder.ToTable("workout_logs", "training");

        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id)
            .HasColumnName("id");

        builder.Property(entity => entity.MemberId)
            .HasColumnName("member_id")
            .IsRequired();

        builder.Property(entity => entity.ProgramId)
            .HasColumnName("program_id")
            .IsRequired();

        builder.Property(entity => entity.ExerciseId)
            .HasColumnName("exercise_id");

        builder.Property(entity => entity.SetsCompleted)
            .HasColumnName("sets_completed");

        builder.Property(entity => entity.RepsCompleted)
            .HasColumnName("reps_completed");

        builder.Property(entity => entity.WeightUsed)
            .HasColumnName("weight_used");

        builder.Property(entity => entity.Duration)
            .HasColumnName("duration");

        builder.Property(entity => entity.LoggedAt)
            .HasColumnName("logged_at")
            .IsRequired();

        builder.Property(entity => entity.CompletedAt)
            .HasColumnName("completed_at")
            .IsRequired();

        builder.Property(entity => entity.Notes)
            .HasColumnName("notes")
            .HasMaxLength(500);

        builder.Property(entity => entity.IsCompleted)
            .HasColumnName("is_completed")
            .IsRequired();
    }
}