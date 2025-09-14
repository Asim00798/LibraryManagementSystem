using LibraryManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Shelf : BaseEntity
    {
        public Guid Id { get; set; } //Primary Key
        public string Code { get; set; } = string.Empty; // e.g., "A1"
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        public ICollection<BookCopy>? BookCopies { get; set; }
    }

}
