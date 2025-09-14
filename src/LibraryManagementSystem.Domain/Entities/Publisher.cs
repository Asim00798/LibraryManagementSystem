using LibraryManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Publisher : BaseEntity
    {
        public Guid Id { get; set; } //Primary Key
        public string Name { get; set; } = string.Empty;
        public Guid? AddressId { get; set; }

        // Navigation
        public Address? Address { get; set; }
        public ICollection<BookTitle>? BookTitles { get; set; }
        public ICollection<BookCopy>? BookCopies { get; set; }

        // Business rules
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException("Publisher Name is required.");
        }
    }

}
