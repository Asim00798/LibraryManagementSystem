#region Usings
using LibraryManagement.Domain.Interfaces;
using LibraryManagementSystem.Application.Features.Interfaces;
using LibraryManagementSystem.Domain.Entities.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#endregion
namespace LibraryManagementSystem.Application.Features.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<string>> GetPermissionsByRoleAsync(string roleName)
        {
            var role = await _unitOfWork.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null) return Enumerable.Empty<string>();

            var rolePermissions = await _unitOfWork.RolePermissions
                .FindAsync(rp => rp.RoleId == role.Id, rp => rp.Permission);

            return rolePermissions.Select(rp => rp.Permission!.Name!);
        }

        public async Task<string> AssignPermissionToRoleAsync(string roleName, string permissionName)
        {
            var role = await _unitOfWork.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            var permission = await _unitOfWork.Permissions.FirstOrDefaultAsync(p => p.Name == permissionName);

            if (role == null || permission == null)
                return "Role or Permission not found.";

            var exists = await _unitOfWork.RolePermissions.AnyAsync(rp =>
                rp.RoleId == role.Id && rp.PermissionId == permission.Id);

            if (exists)
                return $"Permission '{permissionName}' already assigned to role '{roleName}'.";

            await _unitOfWork.RolePermissions.AddAsync(new RolePermission
            {
                RoleId = role.Id,
                PermissionId = permission.Id
            });

            await _unitOfWork.CompleteAsync();
            return $"Permission '{permissionName}' assigned to role '{roleName}'.";
        }

        public async Task<string> RemovePermissionFromRoleAsync(string roleName, string permissionName)
        {
            var role = await _unitOfWork.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            var permission = await _unitOfWork.Permissions.FirstOrDefaultAsync(p => p.Name == permissionName);

            if (role == null || permission == null)
                return "Role or Permission not found.";

            var rolePermission = await _unitOfWork.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == role.Id && rp.PermissionId == permission.Id);

            if (rolePermission == null)
                return $"Permission '{permissionName}' not assigned to role '{roleName}'.";

            await _unitOfWork.RolePermissions.RemoveAsync(rolePermission);
            await _unitOfWork.CompleteAsync();

            return $"Permission '{permissionName}' removed from role '{roleName}'.";
        }

        public async Task<IEnumerable<string>> GetUserPermissionsAsync(User user)
        {
            if (user == null) return Enumerable.Empty<string>();

            // 1️⃣ Get role IDs for the user from the UserRoles repository
            var roleIds = await _unitOfWork.UserRoles
                .Query()
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.RoleId)
                .ToListAsync();

            if (!roleIds.Any()) return Enumerable.Empty<string>();

            // 2️⃣ Get roles (optional, if you need role details)
            var roles = await Task.WhenAll(roleIds.Select(id => _unitOfWork.Roles.GetByIdAsync(id)));

            // 3️⃣ Collect permissions
            var rolePermissions = await _unitOfWork.RolePermissions
                .Query()
                .Include(rp => rp.Permission)
                .Where(rp => roleIds.Contains(rp.RoleId))
                .ToListAsync();

            return rolePermissions
                .Select(rp => rp.Permission!.Name!)
                .Distinct()
                .ToList();
        }

        public async Task<bool> UserHasPermissionAsync(Guid userId, string permissionName)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;

            var permissions = await GetUserPermissionsAsync(user);
            return permissions.Contains(permissionName);
        }
    }
}
