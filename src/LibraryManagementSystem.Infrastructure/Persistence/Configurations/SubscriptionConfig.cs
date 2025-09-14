using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class SubscriptionConfiguration : BaseEntityConfiguration<Subscription>
    {
        public override void Configure(EntityTypeBuilder<Subscription> builder)
        {
            base.Configure(builder);

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(s => s.StartDate).IsRequired();
            builder.Property(s => s.EndDate).IsRequired();
            builder.Property(s => s.IsExtended).IsRequired();
            builder.Property(s => s.Status).IsRequired();

            // -----------------
            // Relationships
            // -----------------

            // Many subscriptions → One user
            builder.HasOne(s => s.User)
                   .WithMany(u => u.Subscriptions)
                   .HasForeignKey(s => s.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Many subscriptions → One membership
            builder.HasOne(s => s.Membership)
                   .WithMany(m => m.Subscriptions)
                   .HasForeignKey(s => s.MembershipId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Many subscriptions → One branch (optional)
            builder.HasOne(s => s.Branch)
                   .WithMany(b => b.Subscriptions)
                   .HasForeignKey(s => s.BranchId)
                   .OnDelete(DeleteBehavior.SetNull);

            // One subscription → Many payment logs
            builder.HasMany(s => s.PaymentLogs)
                   .WithOne(pl => pl.Subscription)
                   .HasForeignKey(pl => pl.SubscriptionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
