using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class ResidencyConfiguration : BaseEntityConfiguration<Residency>
    {
        public override void Configure(EntityTypeBuilder<Residency> builder)
        {
            base.Configure(builder);

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(r => r.ResidencyNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(r => r.ValidUntil).IsRequired(false);

            // Residency → Person (1:1 required)
            builder.HasOne(r => r.Person)
                   .WithOne(p => p.Residency)
                   .HasForeignKey<Residency>(r => r.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Residency → Country (1:N required)
            builder.HasOne(r => r.Country)
                   .WithMany(c => c.Residencies)
                   .HasForeignKey(r => r.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(r => r.PersonId).IsUnique();
            builder.HasIndex(r => r.CountryId);
            builder.HasIndex(r => r.ResidencyNumber).IsUnique();
        }
    }

}
