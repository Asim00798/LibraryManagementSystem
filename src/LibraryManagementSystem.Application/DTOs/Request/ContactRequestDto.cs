using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    public class ContactRequestDto
    {
        // Type of contact (Email, Phone, etc.)
        public ContactType Type { get; set; } = ContactType.Phone;

        // Contact value (e.g., email address or phone number)
        public string Value { get; set; } = string.Empty;

        // Is this the primary contact for the person
        public bool IsPrimary { get; set; } = false;
    }
}
