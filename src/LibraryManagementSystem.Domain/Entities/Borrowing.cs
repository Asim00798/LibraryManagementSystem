using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Borrowing : BaseEntity
    {
        public Guid Id { get; set; } // Primary Key (GUID)

        // -----------------
        // Foreign Keys
        // -----------------
        public Guid UserId { get; set; } // Required
        public Guid BookCopyId { get; set; } // Required
        public Guid? ReservationId { get; set; } // Optional, in case borrowing is from a reservation

        // -----------------
        // Price & Late fees
        // -----------------
        public decimal PricePerDay { get; set; } = Constants.BorrowingConstants.PricePerDay;
        public byte NumberOfLateDays { get; set; } = 0;

        // -----------------
        // Status & Dates
        // -----------------
        public BorrowingStatus Status { get; set; } = BorrowingStatus.Borrowed;
        public DateTime BorrowedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueAt { get; set; }
        public DateTime? ReturnedAt { get; set; }

        // -----------------
        // Navigation Properties
        // -----------------
        public User? User { get; set; }
        public BookCopy? BookCopy { get; set; }
        public Reservation? Reservation { get; set; }
        public ICollection<PaymentLog> PaymentLogs { get; set; } = new List<PaymentLog>();

        // -----------------
        // Validation
        // -----------------
        public override void Validate()
        {
            base.Validate();

            if (DueAt.HasValue && DueAt.Value < BorrowedAt)
                throw new InvalidOperationException("Due date cannot be earlier than BorrowedAt.");

            if (ReturnedAt.HasValue && ReturnedAt.Value < BorrowedAt)
                throw new InvalidOperationException("ReturnedAt cannot be earlier than BorrowedAt.");

            if (!Enum.IsDefined(typeof(BorrowingStatus), Status))
                throw new InvalidOperationException("Invalid borrowing status.");
        }
    }

}
