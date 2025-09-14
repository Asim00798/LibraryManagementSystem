using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.ToTable("Categories");

            // PK
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            // Properties
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Description)
                   .HasMaxLength(500);

            // Indexes
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
