using LibraryManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class BookAuthor : BaseEntity
    {
        // Many <-> Many Join Table between Author and BookTitle
        public int BookTitleId { get; set; }
        public int AuthorId { get; set; }

        // Navigation
        public BookTitle BookTitle { get; set; } = null!;
        public Author Author { get; set; } = null!;

        public override void Validate()
        {
            base.Validate();
        }
    }

}
