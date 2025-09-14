using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    /// <summary>
    /// DTO for creating a new role.
    /// </summary>
    public class RoleRequestDto
    {
        public string RoleName { get; set; } = string.Empty;
    }
}
