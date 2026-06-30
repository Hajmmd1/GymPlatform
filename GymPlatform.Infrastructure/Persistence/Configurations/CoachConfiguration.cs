using GymPlatform.Modules.Membership.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymPlatform.Infrastructure.Persistence.Configurations;

public sealed class CoachConfiguration : IEntityTypeConfiguration<Coach>
{
    public void Configure(EntityTypeBuilder<Coach> builder)
    {
        builder.ToTable("coaches");

        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id)
            .HasColumnName("id");

        builder.Property(entity => entity.GymId)
            .HasColumnName("gym_id")
            .IsRequired();

        builder.Property(entity => entity.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(200)
            .IsRequired();

        builder.OwnsOne(entity => entity.Email, emailBuilder =>
        {
            emailBuilder.Property(email => email.Value)
                .HasColumnName("email")
                .HasMaxLength(320)
                .IsRequired();
        });

        builder.Property(entity => entity.Specialty)
            .HasColumnName("specialty")
            .HasMaxLength(100);

        builder.Property(entity => entity.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.HasIndex("gym_id")
            .HasDatabaseName("ix_coaches_gym_id");

        builder.HasIndex("gym_id", "email")
            .IsUnique()
            .HasDatabaseName("uq_coaches_gym_email");
    }
}
