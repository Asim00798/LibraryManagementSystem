using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Enums
{
    public enum ReservationStatus
    {
        Active = 0,
        Fulfilled = 1,
        Cancelled = 2,
        Expired = 3
    }
}
