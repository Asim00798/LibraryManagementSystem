using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class StaffAttendanceConfiguration : BaseEntityConfiguration<StaffAttendance>
    {
        public override void Configure(EntityTypeBuilder<StaffAttendance> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(sa => sa.Id);
            builder.Property(sa => sa.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(sa => sa.AttendanceTime).IsRequired();
            builder.Property(sa => sa.IsPresent).IsRequired();

            // -----------------
            // Relationships (dependent)
            // -----------------

            // Many StaffAttendance → One Staff
            builder.HasOne(sa => sa.Staff)
                   .WithMany(s => s.StaffAttendances)
                   .HasForeignKey(sa => sa.StaffId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
