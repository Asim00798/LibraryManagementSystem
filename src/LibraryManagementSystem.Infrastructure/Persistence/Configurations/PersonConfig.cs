using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class PersonConfiguration : BaseEntityConfiguration<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.SecondName).HasMaxLength(100).IsRequired(false);
            builder.Property(p => p.ThirdName).HasMaxLength(100).IsRequired(false);
            builder.Property(p => p.DateOfBirth).IsRequired();
            builder.Property(p => p.ProfileImageUrl).HasMaxLength(250).IsRequired(false);

            //Enum conversion
            builder.Property(p => p.Gender)
                   .HasConversion<int>()
                   .IsRequired();

            builder.HasIndex(p => p.AddressId).IsUnique(); //Each Address belongs to one person

            // -----------------
            // Relationships (only on dependent side)
            // -----------------

            // Person -> Nationality (one-to-one, dependent is Nationality)
            builder.HasOne(p => p.Nationality)
                   .WithOne(n => n.Person)
                   .HasForeignKey<Nationality>(n => n.PersonId)
                   .IsRequired(false)           // make the relationship optional
                   .OnDelete(DeleteBehavior.Cascade);

            // Person -> Residency (one-to-one, dependent is Residency)
            builder.HasOne(p => p.Residency)
                   .WithOne(r => r.Person)
                   .HasForeignKey<Residency>(r => r.PersonId)
                   .IsRequired(false)           // make the relationship optional
                   .OnDelete(DeleteBehavior.Cascade); // safe for optional


            // Person -> Address (optional, many-to-one)
            builder.HasOne(p => p.Address)
                   .WithOne(a => a.Person)      // match the navigation on Address
                   .HasForeignKey<Person>(p => p.AddressId) // FK lives in Person
                   .IsRequired(false)           // optional
                   .OnDelete(DeleteBehavior.SetNull);


            // Person -> Contacts (one-to-many, dependent is Contact)
            builder.HasMany(p => p.Contacts)
                   .WithOne(c => c.Person)
                   .HasForeignKey(c => c.PersonId)
                   .IsRequired(false)           // make the relationship optional
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(p => p.LastName);
            builder.HasIndex(p => p.FirstName);
        }
    }
}
