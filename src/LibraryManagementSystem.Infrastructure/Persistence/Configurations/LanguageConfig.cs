using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class LanguageConfiguration : BaseEntityConfiguration<Language>
    {
        public override void Configure(EntityTypeBuilder<Language> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id)
                   .ValueGeneratedOnAdd(); // Auto-increment int

            // Properties
            builder.Property(l => l.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // One-to-Many: Language -> BookTitles
            // Only configuring dependent side in BookTitle, so nothing here.

            // Index
            builder.HasIndex(l => l.Name)
                   .IsUnique();
        }
    }
}
