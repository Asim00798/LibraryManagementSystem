using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Domain.Entities
{
    /// <summary>
    /// Represents a payment record for subscriptions, fines, borrowing fees, or adjustments/refunds.
    /// </summary>
    public class PaymentLog : BaseEntity
    {
        public Guid Id { get; set; } // Primary Key

        // -----------------
        // Foreign Keys
        // -----------------
        public Guid UserId { get; set; } // User who made the payment
        public Guid? BorrowingId { get; set; } // Linked if Fine or Borrowing Fee
        public Guid? SubscriptionId { get; set; } // Linked if Subscription
        public Guid? MembershipId { get; set; } 
        
        // -----------------
        // Payment details
        // -----------------
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public PaymentType PaymentType { get; set; } = PaymentType.None;
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.None;
        public Currency Currency { get; set; } = Currency.AED; // Default ISO currency code
        public string? TransactionReference { get; set; }
        public string? ReceiptNumber { get; set; }
        public string? InvoiceNumber { get; set; }

        // -----------------
        // Refunds / adjustments (self-reference)
        // -----------------
        public Guid? ParentPaymentId { get; set; }
        public PaymentLog? ParentPayment { get; set; }
        public ICollection<PaymentLog> ChildPayments { get; set; } = new List<PaymentLog>();
        public string? Notes { get; set; } // Optional reason for fine
        public FineReason? FineReason { get; set; } = Enums.FineReason.None;

        // -----------------
        // Navigation properties
        // -----------------
        public User? User { get; set; }
        public Borrowing? Borrowing { get; set; }
        public Membership? Membership { get; set; }
        public Subscription? Subscription { get; set; }

        // -----------------
        // Business rules / validation
        // -----------------
        public override void Validate()
        {
            base.Validate();

            // Refunds or adjustments must be negative
            if (PaymentType == PaymentType.Refund || PaymentType == PaymentType.Adjustment)
            {
                if (Amount >= 0)
                    throw new InvalidOperationException("Refund/Adjustment amounts must be negative.");
            }
            // Regular payments must be positive
            else if (Amount <= 0)
            {
                throw new InvalidOperationException("Payment amount must be greater than zero.");
            }

            // Payment date cannot be in the future
            if (PaymentDate > DateTime.UtcNow)
                throw new InvalidOperationException("Payment date cannot be in the future.");

            // PaymentType must be valid
            if (!Enum.IsDefined(typeof(PaymentType), PaymentType))
                throw new InvalidOperationException("Invalid payment type.");

            // Fine-specific validations
            if (PaymentType == PaymentType.Fine)
            {
                if (!FineReason.HasValue)
                    throw new InvalidOperationException("Fine payments must include a FineReason.");
                if (!BorrowingId.HasValue)
                    throw new InvalidOperationException("Fine payments must be linked to a Borrowing.");
            }

            // Subscription-specific validations
            if (PaymentType == PaymentType.Subscription && !MembershipId.HasValue)
                throw new InvalidOperationException("Subscription payments must be linked to a Membership.");

            // Transaction reference must have sufficient length
            if (!string.IsNullOrWhiteSpace(TransactionReference) && TransactionReference.Length < 3)
                throw new InvalidOperationException("Transaction reference is too short.");
        }
    }
}
