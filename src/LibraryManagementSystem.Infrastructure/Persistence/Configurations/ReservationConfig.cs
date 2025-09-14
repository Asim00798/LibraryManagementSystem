using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class ReservationConfiguration : BaseEntityConfiguration<Reservation>
    {
        public override void Configure(EntityTypeBuilder<Reservation> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(r => r.ReservationDate).IsRequired();
            builder.Property(r => r.ExpiryDate).IsRequired();
            builder.Property(r => r.Status).IsRequired();

            // -----------------
            // Relationships (all one-to-one)
            // -----------------

            // Reservation → User (required)
            builder.HasOne(r => r.User)
                   .WithOne() // User side navigation not defined here
                   .HasForeignKey<Reservation>(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Reservation → BookCopy (required)
            builder.HasOne(r => r.BookCopy)
                   .WithMany(bc => bc.Reservations) // if BookCopy can have many reservations
                   .HasForeignKey(r => r.BookCopyId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);


            // Reservation → Borrowing (optional)
            // One-to-one with Borrowing
            builder.HasOne(r => r.Borrowing)
                   .WithOne(b => b.Reservation)
                   .HasForeignKey<Borrowing>(b => b.ReservationId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
