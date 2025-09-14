#region Usings
using LibraryManagementSystem.Application.Features.Interfaces;
using LibraryManagementSystem.Domain.Entities.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#endregion
namespace LibraryManagementSystem.Application.Features.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<string>> GetAllRolesAsync() =>
            await _roleManager.Roles.Select(r => r.Name!).ToListAsync();

        public async Task<IEnumerable<string>> GetRolesByUserAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName)
                       ?? throw new InvalidOperationException("User not found.");
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<string> AssignRoleToUserAsync(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return "User not found.";
            if (!await _roleManager.RoleExistsAsync(roleName)) return $"Role '{roleName}' does not exist.";

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded ? $"Role '{roleName}' assigned to '{userName}'." :
                                     string.Join("; ", result.Errors.Select(e => e.Description));
        }

        public async Task<string> RemoveRoleFromUserAsync(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return "User not found.";
            if (!await _roleManager.RoleExistsAsync(roleName)) return $"Role '{roleName}' does not exist.";

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded ? $"Role '{roleName}' removed from '{userName}'." :
                                     string.Join("; ", result.Errors.Select(e => e.Description));
        }

        public async Task<string> CreateRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
                return $"Role '{roleName}' already exists.";

            var result = await _roleManager.CreateAsync(new Role { Name = roleName });
            return result.Succeeded ? $"Role '{roleName}' created successfully." :
                                     string.Join("; ", result.Errors.Select(e => e.Description));
        }

        public async Task<string> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return $"Role '{roleName}' not found.";

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded ? $"Role '{roleName}' deleted successfully." :
                                     string.Join("; ", result.Errors.Select(e => e.Description));
        }
    }
}
