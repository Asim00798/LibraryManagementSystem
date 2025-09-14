using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    public class RegistrationRequestDto
    {
        public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;
    }
}
