using LibraryManagementSystem.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Security
{
    /// <summary>
    /// Represents the data needed to update a user's profile.
    /// </summary>
    public class UpdateProfileDto
    {
        public PersonRequestDto? Person { get; set; }
        public AddressRequestDto? Address { get; set; }
        public UserRequestDto? User { get; set; }
        public ContactRequestDto? Contact { get; set; }
        public ResidencyRequestDto? Residency { get; set; }
        public NationalityRequestDto? Nationality { get; set; }
    }
}
