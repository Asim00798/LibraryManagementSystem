using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Enums
{
    public enum SubscriptionStatus
    {
        Unknown = 0,
        Active = 1,
        Expired = 2,
        Cancelled = 3,
        Suspended = 4
    }
}
