using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    public class ResidencyRequestDto
    {
        public string ResidencyNumber { get; set; } = string.Empty;
        public DateTime ValidUntil { get; set; } 
    }
}
