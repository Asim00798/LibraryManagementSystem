using LibraryManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Author : BaseEntity
    {
        public int Id { get; set; } //Primary Key
        public string Name { get; set; } = string.Empty;
        public int? AuthorDetailId { get; set; } //FK

        // Navigation
        public AuthorDetail? AuthorDetail { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

        // Business rules
        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException("Author Name is required.");
        }
    }

}
