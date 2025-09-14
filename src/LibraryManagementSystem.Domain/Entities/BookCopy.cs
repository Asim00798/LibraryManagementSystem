using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class BookCopy : BaseEntity
    {
        public Guid Id { get; set; } // Foreign Keys
        public int BookTitleId { get; set; }
        public Guid PublisherId { get; set; }
        public Guid? ShelfId { get; set; }
        public Guid? BranchId { get; set; }

        // Physical details
        public string? Barcode { get; set; }
        public BookCopyStatus CopyStatus { get; set; } = BookCopyStatus.Available;
        public BookPhysicalState PhysicalState { get; set; } = BookPhysicalState.Good;

        // Removed tracking (no extra bool needed)
        public DateTime? RemovedDate { get; set; }

        // Navigation
        public BookTitle? BookTitle { get; set; }
        public Publisher? Publisher { get; set; }
        public Shelf? Shelf { get; set; }
        public Branch? Branch { get; set; }
        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        // Validation
        public override void Validate()
        {
            base.Validate();

            if (BookTitleId <= 0)
                throw new InvalidOperationException("BookCopy must be linked to a valid BookTitle.");

            if (RemovedDate.HasValue && CopyStatus != BookCopyStatus.Removed)
                throw new InvalidOperationException("RemovedDate cannot be set if copy is not removed.");

            // Enum validation
            if (!Enum.IsDefined(typeof(BookCopyStatus), CopyStatus))
                throw new InvalidOperationException($"Invalid CopyStatus: {CopyStatus}");

            if (!Enum.IsDefined(typeof(BookPhysicalState), PhysicalState))
                throw new InvalidOperationException($"Invalid PhysicalState: {PhysicalState}");
        }
    }

}
