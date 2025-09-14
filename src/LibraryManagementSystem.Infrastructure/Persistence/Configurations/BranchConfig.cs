using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class BranchConfiguration : BaseEntityConfiguration<Branch>
    {
        public override void Configure(EntityTypeBuilder<Branch> builder)
        {
            base.Configure(builder);

            builder.ToTable("Branches");

            // PK
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(b => b.Type)
                   .HasConversion<int>() // Enum as int
                   .IsRequired();

            builder.Property(b => b.AddressId)
                   .IsRequired();

            // Indexes
            builder.HasIndex(b => b.Name).IsUnique(false);
        }
    }
}
