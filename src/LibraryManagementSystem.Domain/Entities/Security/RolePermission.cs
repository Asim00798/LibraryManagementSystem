using LibraryManagement.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities.Security
{
    public class RolePermission : BaseEntity
    {
        public Guid Id { get; set; } 

        public Guid RoleId { get; set; }               // add FK explicitly
        public Role Role { get; set; } = null!;

        public Guid PermissionId { get; set; }        
        public Permission Permission { get; set; } = null!;
    }
}
