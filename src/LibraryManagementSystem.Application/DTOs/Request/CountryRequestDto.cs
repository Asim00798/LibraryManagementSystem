using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    public class CountryRequestDto
    {
        public string Name { get; set; } = string.Empty;

        // ISO code (required, 2 or 3 letters)
        public string ISOCode { get; set; } = string.Empty;
    }
}
