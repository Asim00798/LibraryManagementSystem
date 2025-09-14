using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    public class ReturnBookRequestDto
    {
        public string Barcode { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
