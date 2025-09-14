using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Enums
{
    public enum PaymentStatus
    {
        None = 0,
        Pending = 1,
        Completed = 2,
        Failed = 3,
        Refunded = 4
    }
}
