using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class AuthorDetailConfiguration : BaseEntityConfiguration<AuthorDetail>
    {
        public override void Configure(EntityTypeBuilder<AuthorDetail> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(ad => ad.Id);
            builder.Property(ad => ad.Id)
                   .ValueGeneratedOnAdd();

            // Properties
            builder.Property(ad => ad.Alias)
                   .HasMaxLength(100);

            builder.Property(ad => ad.Awards)
                   .HasMaxLength(250);

            builder.Property(ad => ad.BiographyExtended)
                   .HasMaxLength(4000);

            // Relations (dependent side only)
            builder.HasOne(ad => ad.Country)
                   .WithMany()
                   .HasForeignKey(ad => ad.CountryId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(ad => ad.Author)
                   .WithOne(a => a.AuthorDetail)
                   .HasForeignKey<Author>(a => a.AuthorDetailId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Indexes
            builder.HasIndex(ad => ad.CountryId);
        }
    }
}
