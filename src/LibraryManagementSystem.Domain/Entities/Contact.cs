using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public Guid Id { get; set; } // Primary Key (GUID)

        // Foreign key to the associated person
        public Guid PersonId { get; set; }
        public Person Person { get; set; } = null!;

        // Navigation property

        // Type of contact (Email, Phone, etc.)
        public ContactType Type { get; set; } = ContactType.Phone;

        // Contact value (e.g., email address or phone number)
        public string Value { get; set; } = string.Empty;

        // Is this the primary contact for the person
        public bool IsPrimary { get; set; } = false;

        // Navigation: related notifications
        public ICollection<Notification>? Notifications { get; set; }

        // -----------------
        // Business rules / validation
        // -----------------
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(Value))
                throw new InvalidOperationException("Contact value is required.");

            if (!Enum.IsDefined(typeof(ContactType), Type))
                throw new InvalidOperationException("Invalid contact type.");
        }
    }

}
