using GymPlatform.Modules.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymPlatform.Infrastructure.Persistence.Configurations;

public sealed class ExerciseVideoConfiguration : IEntityTypeConfiguration<ExerciseVideo>
{
    public void Configure(EntityTypeBuilder<ExerciseVideo> builder)
    {
        builder.ToTable("exercise_videos", "training");

        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id)
            .HasColumnName("id");

        builder.Property(entity => entity.ExerciseId)
            .HasColumnName("exercise_id")
            .IsRequired();

        builder.Property(entity => entity.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(entity => entity.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);

        builder.Property(entity => entity.VideoUrl)
            .HasColumnName("video_url")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(entity => entity.ThumbnailUrl)
            .HasColumnName("thumbnail_url")
            .HasMaxLength(500);

        builder.Property(entity => entity.DurationSeconds)
            .HasColumnName("duration_seconds");

        builder.Property(entity => entity.CoachId)
            .HasColumnName("coach_id")
            .IsRequired();

        builder.Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(entity => entity.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(entity => entity.IsApproved)
            .HasColumnName("is_approved")
            .IsRequired();

        builder.Property(entity => entity.ViewCount)
            .HasColumnName("view_count")
            .IsRequired();
    }
}