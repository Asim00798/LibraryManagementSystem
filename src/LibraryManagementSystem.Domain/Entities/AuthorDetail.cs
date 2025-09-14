using LibraryManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class AuthorDetail : BaseEntity
    {
        public int Id { get; set; } // Primary Key

        // Foreign Key to Country
        public Guid? CountryId { get; set; }
        public string? Alias { get; set; }
        public string? Awards { get; set; }
        public string? BiographyExtended { get; set; }

        // Navigation Properties
        public Author Author { get; set; } = null!;
        public Country? Country { get; set; } // Optional nationality link

        public override void Validate()
        {
            base.Validate();
        }
    }

}
