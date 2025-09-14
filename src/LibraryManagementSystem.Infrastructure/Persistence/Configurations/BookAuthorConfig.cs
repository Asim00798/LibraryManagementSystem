using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class BookAuthorConfiguration : BaseEntityConfiguration<BookAuthor>
    {
        public override void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            base.Configure(builder);

            // Composite Key (BookTitleId + AuthorId)
            builder.HasKey(ba => new { ba.BookTitleId, ba.AuthorId });

            // Relations (dependent side only)
            builder.HasOne(ba => ba.BookTitle)
                   .WithMany(bt => bt.BookAuthors)
                   .HasForeignKey(ba => ba.BookTitleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ba => ba.Author)
                   .WithMany(a => a.BookAuthors)
                   .HasForeignKey(ba => ba.AuthorId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(ba => ba.BookTitleId);
            builder.HasIndex(ba => ba.AuthorId);
        }
    }
}
