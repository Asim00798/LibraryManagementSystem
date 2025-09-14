using LibraryManagementSystem.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Security
{
    public class FullRegistrationDto
    {
        public AddressRequestDto Address { get; set; } = null!;
        public PersonRequestDto Person { get; set; } = null!;
        public UserRequestDto User { get; set; } = null!;

        //Optional
        public ContactRequestDto? ContactRequestDto { get; set; }
        public ResidencyRequestDto? Residency { get; set; }
        public NationalityRequestDto? Nationality { get; set; }
    }
}
