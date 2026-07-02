using GymPlatform.Modules.Communication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymPlatform.Infrastructure.Persistence.Configurations;

public sealed class CoachAvailabilityConfiguration : IEntityTypeConfiguration<CoachAvailability>
{
    public void Configure(EntityTypeBuilder<CoachAvailability> builder)
    {
        builder.ToTable("CoachAvailability");
        builder.HasKey(ca => ca.Id);

        builder.Property(ca => ca.CoachId)
            .IsRequired();

        builder.Property(ca => ca.GymId)
            .IsRequired();

        builder.Property(ca => ca.StartTime)
            .IsRequired();

        builder.Property(ca => ca.EndTime)
            .IsRequired();

        builder.Property(ca => ca.IsAvailable)
            .IsRequired();

        builder.Property(ca => ca.CreatedAt)
            .IsRequired();

        builder.HasIndex(ca => ca.CoachId);
        builder.HasIndex(ca => new { ca.StartTime, ca.EndTime });
    }
}
