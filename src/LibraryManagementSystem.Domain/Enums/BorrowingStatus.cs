using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Enums
{
    public enum BorrowingStatus
    {
        Borrowed = 0,
        Returned = 1,
        Overdue = 2,
        Lost = 3,
        Cancelled = 4
    }
}
