using GymPlatform.Modules.Membership.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymPlatform.Infrastructure.Persistence.Configurations;

public sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("members");

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

        builder.OwnsOne(entity => entity.Phone, phoneBuilder =>
        {
            phoneBuilder.Property(phone => phone.Value)
                .HasColumnName("phone")
                .HasMaxLength(32);

            phoneBuilder.Navigation(phone => phone)
                .IsRequired(false);
        });

        builder.Property(entity => entity.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(entity => entity.AssignedCoachId)
            .HasColumnName("assigned_coach_id");

        builder.HasIndex("gym_id")
            .HasDatabaseName("ix_members_gym_id");

        builder.HasIndex("gym_id", "email")
            .IsUnique()
            .HasDatabaseName("uq_members_gym_email");

        builder.HasIndex("assigned_coach_id")
            .HasDatabaseName("ix_members_assigned_coach_id");
    }
}
