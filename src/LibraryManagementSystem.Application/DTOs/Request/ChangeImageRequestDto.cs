using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    /// <summary>
    /// DTO for changing personal image
    /// </summary>
    public class ChangeImageRequestDto
    {
        public string ImageUrl { get; set; } = string.Empty;
    }
}
