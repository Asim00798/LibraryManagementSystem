using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Branch : BaseEntity
    {
        /// <summary>
        /// Primary key (sequential GUID for performance).
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Branch name (e.g., "Downtown Library").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of branch (Main, Regional, etc.).
        /// </summary>
        public BranchType Type { get; set; } = BranchType.Main;

        /// <summary>
        /// Foreign key to Address entity (required).
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        /// Navigation to address details.
        /// </summary>
        public Address? Address { get; set; }

        /// <summary>
        /// Events hosted by this branch.
        /// </summary>
        public ICollection<Event> Events { get; set; } = new List<Event>();

        /// <summary>
        /// Optional staff member managing this branch.
        /// </summary>
        public Guid? ManagedByStaffId { get; set; }
        public Staff? ManagedByStaff { get; set; }

        /// <summary>
        /// Staff members working at this branch.
        /// </summary>
        public ICollection<Staff>? Staffs { get; set; }

        /// <summary>
        /// Book copies physically located in this branch.
        /// </summary>
        public ICollection<BookCopy>? BookCopies { get; set; }

        /// <summary>
        /// Shelves of each branch
        /// </summary>
        public ICollection<Shelf>? Shelves { get; set; }
        /// <summary>
        /// Shelves of each branch
        /// </summary>
        public ICollection<Subscription>? Subscriptions { get; set; }

        /// <summary>
        /// Domain validation for entity invariants.
        /// </summary>
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException("Branch name is required.");

            if (AddressId == Guid.Empty)
                throw new InvalidOperationException("Branch must be linked to a valid Address.");
        }
    }

}
