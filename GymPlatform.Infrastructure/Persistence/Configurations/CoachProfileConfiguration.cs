using GymPlatform.Modules.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymPlatform.Infrastructure.Persistence.Configurations;

public sealed class CoachProfileConfiguration : IEntityTypeConfiguration<CoachProfile>
{
    public void Configure(EntityTypeBuilder<CoachProfile> builder)
    {
        builder.ToTable("coach_profiles", "training");

        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id)
            .HasColumnName("id");

        builder.Property(entity => entity.CoachId)
            .HasColumnName("coach_id")
            .IsRequired();

        builder.Property(entity => entity.Bio)
            .HasColumnName("bio")
            .HasMaxLength(2000);

        builder.Property(entity => entity.ProfilePhotoUrl)
            .HasColumnName("profile_photo_url")
            .HasMaxLength(500);

        builder.Property(entity => entity.Rating)
            .HasColumnName("rating");

        builder.Property(entity => entity.RatingCount)
            .HasColumnName("rating_count")
            .IsRequired();

        builder.Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(entity => entity.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.Property(entity => entity.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Ignore(entity => entity.Specialties);

        builder.OwnsMany(entity => entity.Certifications, certification =>
        {
            certification.ToTable("coach_certifications", "training");
            certification.WithOwner().HasForeignKey("coach_profile_id");
            certification.Property(c => c.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            certification.Property(c => c.IssuingOrganization).HasColumnName("issuing_organization").HasMaxLength(200);
            certification.Property(c => c.ExpiresAt).HasColumnName("expires_at");
            certification.Property(c => c.ObtainedAt).HasColumnName("obtained_at").IsRequired();
            certification.Property(c => c.IsVerified).HasColumnName("is_verified").IsRequired();

            certification.HasKey("coach_profile_id", "name");
        });
    }
}