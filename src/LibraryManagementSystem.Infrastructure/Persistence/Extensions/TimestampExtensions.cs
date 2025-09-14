#region Usings
using LibraryManagementSystem.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
namespace LibraryManagementSystem.Infrastructure.Persistence.Extensions
{
    public static class TimestampExtensions
    {
        public static void SetTimestamps(this DbContext context)
        {
            var entries = context.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                if (entry.State == EntityState.Added)
                    entity.CreatedAt = DateTime.UtcNow;

                entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
