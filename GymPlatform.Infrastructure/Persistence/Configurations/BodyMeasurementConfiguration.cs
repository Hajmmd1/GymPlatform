using GymPlatform.Modules.Training.Domain.Entities;
using GymPlatform.Modules.Training.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymPlatform.Infrastructure.Persistence.Configurations;

public sealed class BodyMeasurementConfiguration : IEntityTypeConfiguration<BodyMeasurement>
{
    public void Configure(EntityTypeBuilder<BodyMeasurement> builder)
    {
        builder.ToTable("body_measurements", "training");

        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id)
            .HasColumnName("id");

        builder.Property(entity => entity.MemberId)
            .HasColumnName("member_id")
            .IsRequired();

        builder.Property(entity => entity.Type)
            .HasColumnName("type")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(entity => entity.Value)
            .HasColumnName("value")
            .IsRequired();

        builder.Property(entity => entity.Unit)
            .HasColumnName("unit")
            .HasMaxLength(20);

        builder.Property(entity => entity.MeasuredAt)
            .HasColumnName("measured_at")
            .IsRequired();

        builder.Property(entity => entity.RecordedAt)
            .HasColumnName("recorded_at")
            .IsRequired();

        builder.Property(entity => entity.Notes)
            .HasColumnName("notes")
            .HasMaxLength(500);
    }
}