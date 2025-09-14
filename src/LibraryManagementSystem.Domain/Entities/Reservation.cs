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
    public class Reservation : BaseEntity
    {
        public Guid Id { get; set; } // Primary Key

        // -----------------
        // Foreign Keys
        // -----------------
        public Guid UserId { get; set; } // Required
        public Guid BookCopyId { get; set; } // Required
        public Guid? BorrowingId { get; set; } // Optional, when reservation is fulfilled

        // -----------------
        // Dates & Queue
        // -----------------
        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpiryDate { get; set; }

        // -----------------
        // Status
        // -----------------
        public ReservationStatus Status { get; set; } = ReservationStatus.Active;

        // -----------------
        // Navigation Properties
        // -----------------
        public User? User { get; set; }
        public BookCopy? BookCopy { get; set; }
        public Borrowing? Borrowing { get; set; }

        // -----------------
        // Validation / Business Rules
        // -----------------
        public override void Validate()
        {
            base.Validate();

            if (UserId == Guid.Empty)
                throw new InvalidOperationException("Reservation must be linked to a valid user.");

            if (ExpiryDate <= ReservationDate)
                throw new InvalidOperationException("ExpiryDate must be later than ReservationDate.");

            if (!Enum.IsDefined(typeof(ReservationStatus), Status))
                throw new InvalidOperationException("Invalid reservation status.");
        }
    }

}
