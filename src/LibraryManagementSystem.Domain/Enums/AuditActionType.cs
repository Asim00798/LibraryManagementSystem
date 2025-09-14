using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Enums
{
    namespace LibraryManagement.Domain.Enums
    {
        public enum AuditActionType
        {
            Unknown = 0,
            Created = 1,
            Updated = 2,
            Deleted = 3,
            Viewed = 4
        }
    }

}
