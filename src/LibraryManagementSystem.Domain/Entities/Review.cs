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
    public class Review : BaseEntity
    {
        public Guid Id { get; set; } //Primary Key
        public string Comment { get; set; } = string.Empty;
        public ReviewRate ReviewRate { get; set; } = ReviewRate.None;

        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public int? BookTitleId { get; set; }
        public BookTitle? BookTitle { get; set; }

        public override void Validate()
        {
            base.Validate();

            if ((int)ReviewRate < 1 || (int)ReviewRate > 5)
                throw new InvalidOperationException("Review rate must be between 1 and 5 stars.");
        }
    }

}
