using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : BaseEntityConfiguration<Country>
    {
        public override void Configure(EntityTypeBuilder<Country> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.ISOCode)
                   .IsRequired()
                   .HasMaxLength(3);

            // One-to-many: Country -> Nationalities
            builder.HasMany(c => c.Nationalities)
                   .WithOne(n => n.Country)
                   .HasForeignKey(n => n.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // One-to-many: Country -> Residencies
            builder.HasMany(c => c.Residencies)
                   .WithOne(r => r.Country)
                   .HasForeignKey(r => r.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Index for fast lookup by ISO code
            builder.HasIndex(c => c.ISOCode).IsUnique();
        }
    }
}
