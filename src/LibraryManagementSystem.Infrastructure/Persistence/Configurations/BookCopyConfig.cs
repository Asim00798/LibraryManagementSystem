using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class BookCopyConfiguration : BaseEntityConfiguration<BookCopy>
    {
        public override void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(bc => bc.Id);
            builder.Property(bc => bc.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Enum conversions
            builder.Property(bc => bc.CopyStatus)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(bc => bc.PhysicalState)
                   .HasConversion<int>()
                   .IsRequired();

            // Relations (dependent side only)
            builder.HasOne(bc => bc.BookTitle)
                   .WithMany(bt => bt.BookCopies)
                   .HasForeignKey(bc => bc.BookTitleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bc => bc.Publisher)
                   .WithMany(p => p.BookCopies)
                   .HasForeignKey(bc => bc.PublisherId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bc => bc.Shelf)
                   .WithMany(s => s.BookCopies)
                   .HasForeignKey(bc => bc.ShelfId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bc => bc.Branch)
                   .WithMany(br => br.BookCopies)
                   .HasForeignKey(bc => bc.BranchId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Indexes
            builder.HasIndex(bc => bc.BookTitleId);
            builder.HasIndex(bc => bc.PublisherId);
            builder.HasIndex(bc => bc.ShelfId);
            builder.HasIndex(bc => bc.BranchId);

            // Barcode optional
            builder.Property(bc => bc.Barcode)
                   .HasMaxLength(50)
                   .IsUnicode(false);
        }
    }
}
