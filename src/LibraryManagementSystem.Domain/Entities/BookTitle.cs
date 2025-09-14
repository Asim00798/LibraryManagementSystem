using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class BookTitle : BaseEntity
    {
        public int Id { get; set; } //Primary Key
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;

        // Foreign keys
        public Guid PublisherId { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }

        // Navigation
        public Publisher Publisher { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();
        public Language Language { get; set; } = null!;
        public ICollection<Review>? Reviews { get; set; }

        // Business rules
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(Title))
                throw new InvalidOperationException("Book Title is required.");

            if (string.IsNullOrWhiteSpace(ISBN))
                throw new InvalidOperationException("ISBN is required.");

            if (ISBN.Length is < 10 or > 13)
                throw new InvalidOperationException("ISBN must be 10 or 13 characters long.");
        }
    }

}
