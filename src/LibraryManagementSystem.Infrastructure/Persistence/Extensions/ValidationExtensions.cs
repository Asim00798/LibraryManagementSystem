using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Domain.Entities.Common;
namespace LibraryManagementSystem.Infrastructure.Persistence.Extensions
{
    public static class ValidationExtensions
    {
        public static void ValidateEntities(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>()
                         .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                entry.Entity.Validate();
            }
        }
    }
}
