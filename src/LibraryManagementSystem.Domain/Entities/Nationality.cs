using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Nationality : BaseEntity
    {
        public Guid Id { get; set; }

        // Foreign Keys
        public Guid PersonId { get; set; }
        public Guid CountryId { get; set; }
        public Guid? ResidencyId { get; set; }

        // Identifiers
        public string? NationalIdNumber { get; set; }
        public string? PassportNumber { get; set; }

        // Navigation
        public Person Person { get; set; } = null!;
        public Country Country { get; set; } = null!;
        public Residency? Residency { get; set; } // optional navigation

        // Derived property
        public CitizenshipType CitizenshipType => !string.IsNullOrWhiteSpace(NationalIdNumber)
            ? CitizenshipType.Citizen
            : CitizenshipType.Foreigner;

        // Validation
        public override void Validate()
        {
            base.Validate();

            if (CitizenshipType == CitizenshipType.Citizen)
            {
                if (string.IsNullOrWhiteSpace(NationalIdNumber))
                    throw new InvalidOperationException("Citizen must have a NationalIdNumber.");

                if (!string.IsNullOrWhiteSpace(PassportNumber) || ResidencyId != null)
                    throw new InvalidOperationException("Citizen cannot have PassportNumber or ResidencyId.");
            }
            else // Foreigner
            {
                if (string.IsNullOrWhiteSpace(PassportNumber) && ResidencyId == null)
                    throw new InvalidOperationException("Foreigner must have either PassportNumber or ResidencyId.");
            }
        }
    }

}
