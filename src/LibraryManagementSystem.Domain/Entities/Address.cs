using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Guid Id { get; set; } //Primary Key
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        // Optional because not every country has "states"
        public string? State { get; set; }
        public string? LocationMapUrl { get; set; } = null;
        // Strongly typed instead of string
        public AddressType Type { get; set; } = AddressType.Generic;

        //Navigation
        public Person? Person { get; set; }

        public override void Validate()
        {
             base.Validate();
        }
    }

}
