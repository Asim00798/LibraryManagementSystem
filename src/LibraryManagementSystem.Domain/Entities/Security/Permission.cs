using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Entities.Security;
using System;
using System.Collections.Generic;

namespace LibraryManagement.Domain.Entities.Security
{
    public class Permission : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
