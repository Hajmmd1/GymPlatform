using GymPlatform.Modules.Training.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymPlatform.Infrastructure.Persistence.Configurations;

public sealed class ProgressPhotoConfiguration : IEntityTypeConfiguration<ProgressPhoto>
{
    public void Configure(EntityTypeBuilder<ProgressPhoto> builder)
    {
        builder.ToTable("progress_photos", "training");

        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id)
            .HasColumnName("id");

        builder.Property(entity => entity.MemberId)
            .HasColumnName("member_id")
            .IsRequired();

        builder.Property(entity => entity.PhotoUrl)
            .HasColumnName("photo_url")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(entity => entity.CapturedAt)
            .HasColumnName("captured_at")
            .IsRequired();

        builder.Property(entity => entity.UploadedAt)
            .HasColumnName("uploaded_at")
            .IsRequired();

        builder.Property(entity => entity.ThumbnailUrl)
            .HasColumnName("thumbnail_url")
            .HasMaxLength(500);

        builder.Property(entity => entity.Notes)
            .HasColumnName("notes")
            .HasMaxLength(500);

        builder.Property(entity => entity.IsPrivate)
            .HasColumnName("is_private")
            .IsRequired();

        builder.Property(entity => entity.IsApproved)
            .HasColumnName("is_approved")
            .IsRequired();
    }
}