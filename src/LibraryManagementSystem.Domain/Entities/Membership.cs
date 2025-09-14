using LibraryManagementSystem.Domain.Constants;
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
    public class Membership : BaseEntity
    {
        public Guid Id { get; set; } // Primary Key

        // Enum representing membership type
        public MembershipType MembershipType { get; set; } = MembershipType.Basic;

        // Display name derived from enum (no hardcoding)
        public string DisplayName => MembershipType.ToString();

        // Price assigned automatically based on type (from constants)
        public decimal Price => MembershipType switch
        {
            MembershipType.Basic => MembershipPriceConstants.BasicPrice,
            MembershipType.Premium => MembershipPriceConstants.PremiumPrice,
            MembershipType.Gold => MembershipPriceConstants.GoldPrice,
            _ => throw new InvalidOperationException("Invalid MembershipType")
        };

        // Optional description for extra info
        public string? Description { get; set; }

        // Max books allowed & duration come from constants
        public int MaxBooksAllowed => MembershipConstants.MaxBooksAllowed;
        public int DurationInDays => MembershipConstants.DurationInDays;

        // Active membership flag
        public bool IsActive { get; set; } = true;

        // Navigation: One Membership can have many subscriptions
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<PaymentLog> PaymentLogs { get; set; } = new List<PaymentLog>();

        // Validation (minimal since constants handle most rules)
        public override void Validate()
        {
            base.Validate();

            // Only validate logical requirement
            if (!Enum.IsDefined(typeof(MembershipType), MembershipType))
                throw new InvalidOperationException("Invalid MembershipType.");
        }
    }

}
