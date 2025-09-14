using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class BookTitleConfiguration : BaseEntityConfiguration<BookTitle>
    {
        public override void Configure(EntityTypeBuilder<BookTitle> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(bt => bt.Id);
            builder.Property(bt => bt.Id)
                   .ValueGeneratedOnAdd();

            // Properties
            builder.Property(bt => bt.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(bt => bt.ISBN)
                   .IsRequired()
                   .HasMaxLength(13)
                   .IsUnicode(false);

            // Relations (only on dependent side)
            builder.HasOne(bt => bt.Publisher)
                   .WithMany(p => p.BookTitles)
                   .HasForeignKey(bt => bt.PublisherId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bt => bt.Category)
                   .WithMany(c => c.BookTitles)
                   .HasForeignKey(bt => bt.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bt => bt.Language)
                   .WithMany(l => l.BookTitles)
                   .HasForeignKey(bt => bt.LanguageId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(b => b.ISBN).IsUnique();
        }
    }
}
