using LibraryManagement.Domain.Entities;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : BaseEntityConfiguration<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            // Primary key
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Required fields with max length
            builder.Property(a => a.Street)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(a => a.City)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.State)
                   .HasMaxLength(100);

            // Optional: Add indexes
            builder.HasIndex(a => a.City);
            builder.HasIndex(a => a.State);
        }
    }
}
