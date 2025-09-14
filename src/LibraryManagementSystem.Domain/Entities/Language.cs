using LibraryManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Language : BaseEntity
    {
        /// <summary>
        /// Primary key for the Language entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the language (e.g., "English", "Arabic").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property to the collection of book titles
        /// written in this language.
        /// </summary>
        public ICollection<BookTitle> BookTitles { get; set; } = new List<BookTitle>();

        /// <summary>
        /// Validates business rules for the Language entity.
        /// </summary>
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException("Language name is required.");

            if (Name.Length > 100)
                throw new InvalidOperationException("Language name cannot exceed 100 characters.");
        }
    }

}
