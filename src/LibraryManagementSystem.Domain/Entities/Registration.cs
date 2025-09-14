using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Registration : BaseEntity
    {
        public Guid Id { get; set; } // PK (GUID)

        public Guid PersonId { get; set; } // Required FK → Person
        public Person Person { get; set; } = null!;

        public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;

        // Navigation to the created User (1–1). User is created after Registration is saved.
        public User? User { get; set; }

        public override void Validate()
        {
            base.Validate();

            if (PersonId == Guid.Empty)
                throw new InvalidOperationException("Registration must reference a valid Person.");
        }
    }

}
