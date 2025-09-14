using LibraryManagementSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities.Security
{
    public class User : IdentityUser<Guid>
    {
        // Required 1–1 link: User must originate from Registration
        public Guid RegistrationId { get; set; }
        public Registration Registration { get; set; } = null!;

        // Subscriptions etc.
        public ICollection<Subscription>? Subscriptions { get; set; }
        public ICollection<PaymentLog>? PaymentLogs { get; set; }
        public ICollection<Borrowing>? Borrowings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }

}
