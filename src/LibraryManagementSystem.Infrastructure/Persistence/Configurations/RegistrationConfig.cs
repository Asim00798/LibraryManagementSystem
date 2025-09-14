using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class RegistrationConfiguration : BaseEntityConfiguration<Registration>
    {
        public override void Configure(EntityTypeBuilder<Registration> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(r => r.Status).IsRequired();

            // -----------------
            // Relationships (dependent side only)
            // -----------------

            // Registration → Person (required one-to-one)
            builder.HasOne(r => r.Person)
                   .WithOne() // Person side navigation not defined
                   .HasForeignKey<Registration>(r => r.PersonId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Registration → User (optional one-to-one)
            builder.HasOne(r => r.User)
                   .WithOne(u => u.Registration)
                   .HasForeignKey<User>(u => u.RegistrationId)
                   .IsRequired(false)                   // explicitly optional
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
