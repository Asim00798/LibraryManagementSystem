using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Enums
{
    public enum PaymentType
    {
        None = 0,
        Regular = 1,
        Fine = 2,
        Subscription = 3,
        Refund = 4,
        Adjustment = 5,
        Borrowing = 6
    }
}
