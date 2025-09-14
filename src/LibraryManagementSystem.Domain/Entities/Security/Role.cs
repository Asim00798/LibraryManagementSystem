using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Domain.Entities.Security
{
    public class Role : IdentityRole<Guid>
    {
        public string? Description { get; set; }

        // Navigation properties
        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
