using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    public class NationalityRequestDto
    {
        public string? NationalIdNumber { get; set; }
        public string? PassportNumber { get; set; }

        // Derived property
        public CitizenshipType CitizenshipType => !string.IsNullOrWhiteSpace(NationalIdNumber)
            ? CitizenshipType.Citizen
            : CitizenshipType.Foreigner;
    }
}
