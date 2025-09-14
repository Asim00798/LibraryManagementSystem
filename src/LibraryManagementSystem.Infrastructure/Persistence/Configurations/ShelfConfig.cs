using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class ShelfConfiguration : BaseEntityConfiguration<Shelf>
    {
        public override void Configure(EntityTypeBuilder<Shelf> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(s => s.Code).IsRequired();

            // -----------------
            // Relationships (Shelf is dependent)
            // -----------------

            // Shelf → Branch (required)
            builder.HasOne(s => s.Branch)
                   .WithMany(b => b.Shelves)
                   .HasForeignKey(s => s.BranchId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
