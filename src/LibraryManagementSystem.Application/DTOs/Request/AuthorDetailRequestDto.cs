using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    public class AuthorDetailRequestDto
    {
        public string? Alias { get; set; }
        public string? Awards { get; set; }
        public string? BiographyExtended { get; set; }
    }
}
