using LibraryManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class StaffAttendance : BaseEntity
    {
        public Guid Id { get; set; } // Primary Key
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; } = null!;
        public DateTime AttendanceTime { get; set; } = DateTime.UtcNow;
        public bool IsPresent { get; set; }

        public override void Validate()
        {
            base.Validate();

            if (AttendanceTime > DateTime.UtcNow.AddMinutes(5))
                throw new InvalidOperationException("Attendance time cannot be in the future.");
        }
    }

}
