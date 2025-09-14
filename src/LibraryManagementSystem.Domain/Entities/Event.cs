using LibraryManagementSystem.Domain.Entities.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Event : BaseEntity
    {
        /// <summary>
        /// Unique identifier for the event (Primary Key).
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the event (required).
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Detailed description of the event.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Start date and time of the event (required).
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// End date and time of the event (required).
        /// Must be after <see cref="StartTime"/>.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Optional foreign key to the branch hosting the event.
        /// </summary>
        public Guid? BranchId { get; set; }

        /// <summary>
        /// Navigation property to the hosting branch.
        /// </summary>
        public Branch? Branch { get; set; }

        /// <summary>
        /// Collection of staff assigned to actively participate in the event.
        /// </summary>
        public ICollection<Staff> EventActiveStaff { get; set; } = new List<Staff>();

        /// <summary>
        /// Enforces business rules for Event entity.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when Name is empty or StartTime is not before EndTime.
        /// </exception>
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException("Event Name is required.");

            if (StartTime >= EndTime)
                throw new InvalidOperationException("Event StartTime must be before EndTime.");
        }
    }

}
