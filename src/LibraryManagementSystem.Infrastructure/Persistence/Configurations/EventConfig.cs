using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class EventConfiguration : BaseEntityConfiguration<Event>
    {
        public override void Configure(EntityTypeBuilder<Event> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.Description)
                   .IsRequired()
                   .HasMaxLength(2000);

            builder.Property(e => e.StartTime)
                   .IsRequired();

            builder.Property(e => e.EndTime)
                   .IsRequired();

            // Optional relationship to Branch
            builder.HasOne(e => e.Branch)
                   .WithMany(b => b.Events) // Branch is principal
                   .HasForeignKey(e => e.BranchId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Many-to-Many: Event <-> Staff
            builder.HasMany(e => e.EventActiveStaff)
                   .WithMany(s => s.EventsParticipated)
                   .UsingEntity<Dictionary<string, object>>(
                       "EventStaff",  // join table name
                       j => j.HasOne<Staff>()
                             .WithMany()
                             .HasForeignKey("StaffId")
                             .OnDelete(DeleteBehavior.Cascade),
                       j => j.HasOne<Event>()
                             .WithMany()
                             .HasForeignKey("EventId")
                             .OnDelete(DeleteBehavior.Cascade),
                       j =>
                       {
                           j.HasKey("EventId", "StaffId"); // composite primary key
                           j.ToTable("EventStaff");       // optional table name
                       });



            // Indexes for search or filtering
            builder.HasIndex(e => e.StartTime);
            builder.HasIndex(e => e.EndTime);
        }
    }
}
