using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Security
{
    /// <summary>
    /// DTO for assigning or removing permissions
    /// </summary>
    public class AssignPermissionRequestDto
    {
        public string RoleName { get; set; } = string.Empty;
        public string PermissionName { get; set; } = string.Empty;
    }
}
