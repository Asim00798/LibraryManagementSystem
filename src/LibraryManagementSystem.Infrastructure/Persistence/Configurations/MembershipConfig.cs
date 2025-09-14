using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class MembershipConfiguration : BaseEntityConfiguration<Membership>
    {
        public override void Configure(EntityTypeBuilder<Membership> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(m => m.MembershipType)
                   .IsRequired()
                   .HasConversion<int>(); // Enum stored as int

            builder.Property(m => m.Description)
                   .HasMaxLength(500);

            builder.Property(m => m.IsActive)
                   .IsRequired();

            // Indexes
            builder.HasIndex(m => m.MembershipType)
                   .IsUnique(false);

            // Relations to dependents (Subscriptions, Users, PaymentLogs) are configured on dependent entities
        }
    }
}
