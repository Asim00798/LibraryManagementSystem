using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class ContactConfiguration : BaseEntityConfiguration<Contact>
    {
        public override void Configure(EntityTypeBuilder<Contact> builder)
        {
            base.Configure(builder);

            builder.ToTable("Contacts");

            // Primary Key
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(c => c.Value)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.Type)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(c => c.IsPrimary)
                   .IsRequired();

            // Relationships
            builder.HasOne(c => c.Person)
                   .WithMany(p => p.Contacts) // Person has many contacts
                   .HasForeignKey(c => c.PersonId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(c => new { c.PersonId, c.Type })
                   .IsUnique();
        }
    }
}
