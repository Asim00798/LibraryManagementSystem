using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Person : BaseEntity
    {
        public Guid Id { get; set; } //Primary Key

        // Personal Info
        public string FirstName { get; set; } = string.Empty;
        public string? SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public GenderType Gender { get; set; } = GenderType.Unknown;

        // Optional personal image
        public string? ProfileImageUrl { get; set; }

        // Optional foreign keys
        public Guid? AddressId { get; set; }

        // Navigation
        public Nationality? Nationality { get; set; }
        public Address? Address { get; set; }
        public ICollection<Contact>? Contacts { get; set; }
        public Residency? Residency { get; set; }

        // Business rules
        public override void Validate()
        {
            base.Validate();

            if (DateOfBirth > DateTime.UtcNow)
                throw new InvalidOperationException("DateOfBirth cannot be in the future.");

            if (DateOfBirth < DateTime.UtcNow.AddYears(-120))
                throw new InvalidOperationException("Person cannot be older than 120 years.");
        }
    }

}
