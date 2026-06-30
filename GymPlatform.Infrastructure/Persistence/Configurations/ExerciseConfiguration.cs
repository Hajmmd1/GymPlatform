using GymPlatform.Modules.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymPlatform.Infrastructure.Persistence.Configurations;

public sealed class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("exercises", "training");

        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id)
            .HasColumnName("id");

        builder.Property(entity => entity.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(entity => entity.Description)
            .HasColumnName("description")
            .HasMaxLength(1000);

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

        builder.Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(entity => entity.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.OwnsMany(entity => entity.PrimaryMuscleGroups, muscleGroup =>
        {
            muscleGroup.ToTable("exercise_primary_muscle_groups", "training");
            muscleGroup.WithOwner().HasForeignKey("exercise_id");
            muscleGroup.Property(m => m.Value).HasColumnName("muscle_group");
        });

        builder.OwnsMany(entity => entity.SecondaryMuscleGroups, muscleGroup =>
        {
            muscleGroup.ToTable("exercise_secondary_muscle_groups", "training");
            muscleGroup.WithOwner().HasForeignKey("exercise_id");
            muscleGroup.Property(m => m.Value).HasColumnName("muscle_group");
        });

        builder.OwnsOne(entity => entity.RequiredEquipment, equipment =>
        {
            equipment.ToTable("exercises", "training");
            equipment.Property(e => e.Value).HasColumnName("required_equipment");
        });
    }
}