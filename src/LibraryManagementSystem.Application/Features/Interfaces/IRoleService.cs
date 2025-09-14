using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Features.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<string>> GetAllRolesAsync();
        Task<IEnumerable<string>> GetRolesByUserAsync(string userName);
        Task<string> AssignRoleToUserAsync(string userName, string roleName);
        Task<string> RemoveRoleFromUserAsync(string userName, string roleName);
        Task<string> CreateRoleAsync(string roleName);
        Task<string> DeleteRoleAsync(string roleName);
    }
}
