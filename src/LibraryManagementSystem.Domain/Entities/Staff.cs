using LibraryManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Staff : BaseEntity
    {
        public Guid Id { get; set; } // Primary Key

        // -----------------
        // Foreign Keys
        // -----------------
        public Guid PersonId { get; set; }
        public Guid BranchId { get; set; }

        // -----------------
        // Staff Info
        // -----------------
        public string EmployeeNumber { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public string Position { get; set; } = string.Empty;

        // ---------------------
        // Navigation Properties
        // ---------------------
        public Person Person { get; set; } = null!; // required
        public Branch Branch { get; set; } = null!; // required

        // Many-to-many: Staff manages multiple branches
        public ICollection<Branch> ManagedBranches { get; set; } = new List<Branch>();
        public ICollection<StaffAttendance>? StaffAttendances { get; set; }
        public ICollection<Event> EventsParticipated { get; set; } = new List<Event>();

        // -----------------
        // Business Rules / Validation
        // -----------------
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(EmployeeNumber))
                throw new InvalidOperationException("EmployeeNumber is required.");

            if (HireDate > DateTime.UtcNow)
                throw new InvalidOperationException("HireDate cannot be in the future.");

            if (string.IsNullOrWhiteSpace(Position))
                throw new InvalidOperationException("Position is required.");
        }
    }

}
