using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    public class PaymentlogRequestDto
    {
        // -----------------
        // Payment details
        // -----------------
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public PaymentType PaymentType { get; set; } = PaymentType.None;
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.None;
        public Currency Currency { get; set; } = Currency.AED; // Default ISO currency code
        public string? TransactionReference { get; set; }
        public string? ReceiptNumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? Notes { get; set; } // Optional reason for fine
        public FineReason? FineReason { get; set; }
    }
}
