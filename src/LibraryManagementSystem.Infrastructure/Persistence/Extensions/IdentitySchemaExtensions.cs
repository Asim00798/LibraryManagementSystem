#region Usings
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Entities.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
namespace LibraryManagementSystem.Infrastructure.Persistence.Extensions
{
    public static class IdentitySchemaConfig
    {
        public static void RenameIdentityTables(this ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users", "Security");
            builder.Entity<Role>().ToTable("Roles", "Security");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", "Security");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", "Security");

            builder.Entity<Permission>().ToTable("Permissions", "Security");
            builder.Entity<RolePermission>().ToTable("RolePermissions", "Security");
        }
    }
}
