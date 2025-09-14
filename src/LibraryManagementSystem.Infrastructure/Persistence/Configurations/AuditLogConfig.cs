using LibraryManagement.Domain.Entities;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Persistence.Configurations
{
    public class AuditLogConfiguration : BaseEntityConfiguration<AuditLog>
    {
        public override void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            base.Configure(builder);

            // Primary key
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // EntityName required, max length
            builder.Property(a => a.EntityName)
                   .IsRequired()
                   .HasMaxLength(100);

            // EntityId required
            builder.Property(a => a.EntityId)
                   .IsRequired()
                   .HasMaxLength(36*5);

            // ActionType as int
            builder.Property(a => a.ActionType)
                   .HasConversion<int>()
                   .IsRequired();

            // ActionPerformedAt required
            builder.Property(a => a.ActionPerformedAt)
                   .IsRequired();

            // Optional: Changes as JSON string
            builder.Property(a => a.Changes)
                   .HasColumnType("nvarchar(max)")
                   .IsRequired(false);

            // Indexes
            builder.HasIndex(a => a.EntityName);
            builder.HasIndex(a => a.ActionPerformedByUserId);
            builder.HasIndex(a => a.ActionType);
        }
    }
}
