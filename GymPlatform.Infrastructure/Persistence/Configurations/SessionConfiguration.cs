using GymPlatform.Modules.Communication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymPlatform.Infrastructure.Persistence.Configurations;

public sealed class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(s => s.SessionType)
            .IsRequired();

        builder.Property(s => s.StartTime)
            .IsRequired();

        builder.Property(s => s.EndTime)
            .IsRequired();

        builder.Property(s => s.MaxCapacity)
            .IsRequired();

        builder.Property(s => s.BookedCount)
            .IsRequired();

        builder.Property(s => s.IsCancelled)
            .IsRequired();

        builder.Property(s => s.GymId)
            .IsRequired();

        builder.Property(s => s.CoachId)
            .IsRequired();

        builder.Property(s => s.RoomId)
            .IsRequired(false);

        builder.Property(s => s.CreatedAt)
            .IsRequired();

        builder.HasIndex(s => s.GymId);
        builder.HasIndex(s => s.CoachId);
        builder.HasIndex(s => s.RoomId);
        builder.HasIndex(s => new { s.StartTime, s.EndTime });
    }
}
