using LibraryManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Residency : BaseEntity
    {
        public Guid Id { get; set; } // Primary Key

        // -----------------
        // Foreign Keys
        // -----------------
        public Guid PersonId { get; set; }
        public Guid CountryId { get; set; }

        // -----------------
        // Residency Details
        // -----------------
        public string ResidencyNumber { get; set; } = string.Empty;
        public DateTime? ValidUntil { get; set; } // Optional expiration date

        // -----------------
        // Navigation Properties
        // -----------------
        public Person Person { get; set; } = null!;
        public Country Country { get; set; } = null!;
        public Nationality? Nationality { get; set; }

        // -----------------
        // Validation / Business rules
        // -----------------
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(ResidencyNumber))
                throw new InvalidOperationException("ResidencyNumber is required.");

            if (ValidUntil.HasValue && ValidUntil.Value < DateTime.UtcNow)
                throw new InvalidOperationException("ValidUntil cannot be in the past.");
        }
    }

}
