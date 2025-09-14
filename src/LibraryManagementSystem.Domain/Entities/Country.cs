using LibraryManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Country : BaseEntity
    {
        public Guid Id { get; set; } // Primary Key (GUID)

        // Country Name (required)
        public string Name { get; set; } = string.Empty;

        // ISO code (required, 2 or 3 letters)
        public string ISOCode { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<Nationality>? Nationalities { get; set; }
        public ICollection<Residency>? Residencies { get; set; }

        // -----------------
        // Validation / Business Rules
        // -----------------
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException("Country Name is required.");

            if (string.IsNullOrWhiteSpace(ISOCode))
                throw new InvalidOperationException("Country ISOCode is required.");
        }
    }

}
