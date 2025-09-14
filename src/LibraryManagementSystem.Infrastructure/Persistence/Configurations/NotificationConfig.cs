using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Common;
using LibraryManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class NotificationConfiguration : BaseEntityConfiguration<Notification>
    {
        public override void Configure(EntityTypeBuilder<Notification> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(n => n.Message)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(n => n.Status)
                   .HasConversion<int>() // Enum to int
                   .IsRequired();

            builder.Property(n => n.IsRead)
                   .IsRequired();

            builder.Property(n => n.DeliveredAt)
                   .IsRequired(false);

            builder.Property(n => n.ReadAt)
                   .IsRequired(false);

            // Relationships
            builder.HasOne(n => n.Contact)
                   .WithMany(c => c.Notifications) // Contact can have many notifications
                   .HasForeignKey(n => n.ContactId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(n => n.ContactId);
            builder.HasIndex(n => n.Status);
        }
    }
}
