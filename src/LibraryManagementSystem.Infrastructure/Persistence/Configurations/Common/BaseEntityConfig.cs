using LibraryManagement.Domain;
using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // Primary key convention if exists (auto PK is handled in child)
            // Auditing
            builder.Property(e => e.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.Property(e => e.UpdatedAt)
                   .IsRequired(false);

            builder.Property(e => e.CreatedBy)
                   .IsRequired(false);

            builder.Property(e => e.UpdatedBy)
                   .IsRequired(false);

            // Soft delete
            builder.Property(e => e.IsDeleted)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(e => e.DeletedAt)
                   .IsRequired(false);

            builder.Property(e => e.DeletedBy)
                   .IsRequired(false);

            // Global query filter for soft delete
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
