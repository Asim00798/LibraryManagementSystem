using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class BorrowingConfiguration : BaseEntityConfiguration<Borrowing>
    {
        public override void Configure(EntityTypeBuilder<Borrowing> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(b => b.PricePerDay)
                   .HasPrecision(18, 2);

            builder.Property(b => b.NumberOfLateDays);

            builder.Property(b => b.Status)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(b => b.BorrowedAt)
                   .IsRequired();

            builder.Property(b => b.DueAt);
            builder.Property(b => b.ReturnedAt);

            // Relations (only as per entity)
            builder.HasOne(b => b.User)
                   .WithMany(u => u.Borrowings)
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.BookCopy)
                   .WithMany(bc => bc.Borrowings) // if BookCopy can have many borrowings
                   .HasForeignKey(b => b.BookCopyId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            // One-to-one with Reservation
            builder.HasOne(b => b.Reservation)
                   .WithOne(r => r.Borrowing)
                   .HasForeignKey<Borrowing>(b => b.ReservationId) // FK on Borrowing
                   .IsRequired(false) // optional
                   .OnDelete(DeleteBehavior.SetNull);


            // Indexes
            builder.HasIndex(b => b.UserId);
            builder.HasIndex(b => b.BookCopyId);
            builder.HasIndex(b => b.ReservationId);
        }
    }
}
