using LibraryManagementSystem.Domain.Entities.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Features.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<string>> GetPermissionsByRoleAsync(string roleName);
        Task<string> AssignPermissionToRoleAsync(string roleName, string permissionName);
        Task<string> RemovePermissionFromRoleAsync(string roleName, string permissionName);
        Task<IEnumerable<string>> GetUserPermissionsAsync(User user);
        Task<bool> UserHasPermissionAsync(Guid userId, string permissionName);
    }
}
