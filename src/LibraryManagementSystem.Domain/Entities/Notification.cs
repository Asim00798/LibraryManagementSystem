using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Notification : BaseEntity
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Foreign key to the associated contact (email, phone, etc.)
        /// </summary>
        public Guid ContactId { get; set; }

        /// <summary>
        /// Navigation property to the contact
        /// </summary>
        public Contact Contact { get; set; } = null!;

        /// <summary>
        /// Notification message content
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Status of the message (e.g., Sent, Failed)
        /// </summary>
        public MessageStatus Status { get; set; } = MessageStatus.None;

        /// <summary>
        /// Whether the notification has been read by the recipient
        /// </summary>
        public bool IsRead { get; set; } = false;

        /// <summary>
        /// When the message was delivered (optional)
        /// </summary>
        public DateTime? DeliveredAt { get; set; }

        /// <summary>
        /// When the message was read (optional)
        /// </summary>
        public DateTime? ReadAt { get; set; }

        /// <summary>
        /// Validates business rules for notifications
        /// </summary>
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(Message))
                throw new InvalidOperationException("Notification Message cannot be empty.");
        }
    }

}
