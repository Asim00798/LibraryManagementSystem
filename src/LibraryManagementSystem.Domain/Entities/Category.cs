using LibraryManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; } //Primary Key
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Navigation
        public ICollection<BookTitle> BookTitles { get; set; } = new List<BookTitle>();

        // Business rules
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException("Category Name is required.");
        }
    }

}
