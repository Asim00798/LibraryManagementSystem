using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    public class UpdateLanguageRequest
    {
        public string Name { get; set; } = string.Empty;
        public string NewName {  get; set; } = string.Empty;
    }
}
