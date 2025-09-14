using LibraryManagement.Infrastructure.Persistence.Extensions;
using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryManagementSystem.Infrastructure.Persistence.Extensions
{
    public static class ApplyExtensions
    {
        /// <summary>
        /// Applies common persistence logic before saving:
        /// - Validates entities (business rules).
        /// - Sets timestamps (CreatedAt, UpdatedAt).
        /// - Converts hard deletes to soft deletes.
        /// - Creates audit logs for entity changes.
        /// </summary>
        /// <param name="context">DbContext instance</param>
        /// <param name="currentUserId">Current user performing the action</param>
        public static void ApplyPersistenceLogic(this DbContext context, Guid? currentUserId)
        {
            // 1. Validate entities against business rules
            context.ValidateEntities();

            // 2. Set timestamps
            context.SetTimestamps();

            // 3. Convert Remove() into SoftDelete if entity supports it
            context.ApplySoftDelete();

            // 4. Audit logging
            context.ApplyAuditLogging(currentUserId);
        }
    }
}
