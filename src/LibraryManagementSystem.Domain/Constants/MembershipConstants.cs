using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Constants
{
    public static class MembershipConstants
    {
        // How many books a member can borrow at once
        public const int MaxBooksAllowed = 5;

        // Membership duration (in days)
        public const int DurationInDays = 30;
    }
}
