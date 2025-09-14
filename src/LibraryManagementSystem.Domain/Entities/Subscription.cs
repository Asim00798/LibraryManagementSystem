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
    public class Subscription : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid MembershipId { get; set; }
        public Membership Membership { get; set; } = null!;
        public Guid? BranchId { get; set; } // Optional since service can be online
        public Branch? Branch { get; set; }
        public ICollection<PaymentLog> PaymentLogs { get; set; } = new List<PaymentLog>();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsExtended { get; set; } = false;
        public DateTime? ExtendedUntil { get; set; }
        public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Unknown;

        public override void Validate()
        {
            base.Validate();

            if (StartDate >= EndDate)
                throw new InvalidOperationException("StartDate must be before EndDate.");

            if (IsExtended && !ExtendedUntil.HasValue)
                throw new InvalidOperationException("ExtendedUntil required if IsExtended is true.");

            if (ExtendedUntil.HasValue && ExtendedUntil <= EndDate)
                throw new InvalidOperationException("ExtendedUntil must be after EndDate.");
        }
    }

}
