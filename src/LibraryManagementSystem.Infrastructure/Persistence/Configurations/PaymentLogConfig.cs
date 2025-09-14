using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class PaymentLogConfiguration : BaseEntityConfiguration<PaymentLog>
    {
        public override void Configure(EntityTypeBuilder<PaymentLog> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(pl => pl.Amount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(p => p.PaymentDate).IsRequired();
            builder.Property(p => p.PaymentType)
                   .HasConversion<int>()
                   .IsRequired();
            builder.Property(p => p.PaymentMethod)
                   .HasConversion<int>()
                   .IsRequired();
            builder.Property(p => p.PaymentStatus)
                   .HasConversion<int>()
                   .IsRequired();
            builder.Property(p => p.Currency)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(p => p.TransactionReference).HasMaxLength(100).IsRequired(false);
            builder.Property(p => p.ReceiptNumber).HasMaxLength(50).IsRequired(false);
            builder.Property(p => p.InvoiceNumber).HasMaxLength(50).IsRequired(false);
            builder.Property(p => p.Notes).HasMaxLength(500).IsRequired(false);

            // -----------------
            // Relationships (only on dependent side)
            // -----------------

            // PaymentLog -> User (many-to-one)
            builder.HasOne(p => p.User)
                   .WithMany(u => u.PaymentLogs)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // PaymentLog -> Borrowing (optional many-to-one)
            builder.HasOne(p => p.Borrowing)
                   .WithMany(b => b.PaymentLogs)
                   .HasForeignKey(p => p.BorrowingId)
                   .OnDelete(DeleteBehavior.SetNull);

            // PaymentLog -> Membership (optional many-to-one)
            builder.HasOne(p => p.Membership)
                   .WithMany(m => m.PaymentLogs)
                   .HasForeignKey(p => p.MembershipId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Self-referencing for refunds/adjustments (one-to-many)
            builder.HasOne(p => p.ParentPayment)
                   .WithMany(p => p.ChildPayments)
                   .HasForeignKey(p => p.ParentPaymentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes for faster lookups
            builder.HasIndex(p => p.UserId);
            builder.HasIndex(p => p.BorrowingId);
            builder.HasIndex(p => p.MembershipId);
            builder.HasIndex(p => p.PaymentType);
            builder.HasIndex(p => p.PaymentStatus);
        }
    }
}
