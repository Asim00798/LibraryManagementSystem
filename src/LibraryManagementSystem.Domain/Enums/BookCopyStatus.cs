using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Enums
{
    public enum BookCopyStatus
    {
        Available = 0,
        Borrowed = 1,
        Reserved = 2,
        Removed = 3
    }
}
