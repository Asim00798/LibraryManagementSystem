using LibraryManagementSystem.Application.DTOs.Security;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    public class SubscriptionRequestDto
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public UserRequestDto User { get; set; } = null!;
        public MembershipType MembershipType { get; set; } = MembershipType.Basic;
        public string BranchName { get; set; } = string.Empty;
        public PaymentlogRequestDto PaymentlogRequest { get; set; } = null!;

    }
}
