using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    /// <summary>
    /// DTO for assigning/removing roles to/from a user.
    /// </summary>
    public class AssignRoleRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
