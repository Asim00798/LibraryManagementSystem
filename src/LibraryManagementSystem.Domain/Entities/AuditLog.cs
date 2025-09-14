using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Enums.LibraryManagement.Domain.Enums;
using LibraryManagementSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public Guid Id { get; set; } // Primary Key

        /// <summary>
        /// Name of the entity being audited (e.g., "Author", "User").
        /// </summary>
        public string EntityName { get; set; } = string.Empty;

        /// <summary>
        /// Identifier of the audited entity (stored as string for flexibility).
        /// </summary>
        public string EntityId { get; set; } = string.Empty;

        /// <summary>
        /// Type of action (Create, Update, Delete, etc.).
        /// </summary>
        public AuditActionType ActionType { get; set; } = AuditActionType.Created;

        /// <summary>
        /// User who performed the action (business action performer).
        /// </summary>
        public Guid? ActionPerformedByUserId { get; set; }

        /// <summary>
        /// When the action occurred (not when the log was written).
        /// </summary>
        public DateTime ActionPerformedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Optional serialized snapshot of changes.
        /// </summary>
        public string? Changes { get; set; }

        // Navigation property (who did the action)
        public User? ActionPerformedBy { get; set; }

        /// <summary>
        /// Validation to ensure audit log integrity.
        /// </summary>
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(EntityName))
                throw new InvalidOperationException("EntityName is required.");

            if (ActionPerformedAt > DateTime.UtcNow)
                throw new InvalidOperationException("ActionPerformedAt cannot be in the future.");
        }
    }

}
