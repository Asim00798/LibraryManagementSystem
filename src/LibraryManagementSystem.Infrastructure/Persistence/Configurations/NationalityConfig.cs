using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Persistence.Configurations
{
    public class NationalityConfiguration : BaseEntityConfiguration<Nationality>
    {
        public override void Configure(EntityTypeBuilder<Nationality> builder)
        {
            base.Configure(builder);

            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            // Person (1:1)
            builder.HasOne(n => n.Person)
                   .WithOne(p => p.Nationality)
                   .HasForeignKey<Nationality>(n => n.PersonId)
                   .OnDelete(DeleteBehavior.Cascade);
            // Country (1:N)
            builder.HasOne(n => n.Country)
                   .WithMany(c => c.Nationalities)
                   .HasForeignKey(n => n.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);
            // Residency (1:N)
            builder.HasOne(n => n.Residency)
                   .WithOne(r => r.Nationality)   // <-- link the navigation back
                   .HasForeignKey<Nationality>(n => n.ResidencyId) // FK lives in Nationality
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);

            //Properities
            builder.Property(n => n.NationalIdNumber)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(n => n.PassportNumber)
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Ignore(n => n.CitizenshipType);
        }
    }

}
