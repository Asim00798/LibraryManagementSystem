using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class StaffConfiguration : BaseEntityConfiguration<Staff>
    {
        public override void Configure(EntityTypeBuilder<Staff> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(s => s.EmployeeNumber).IsRequired();
            builder.Property(s => s.Position).IsRequired();
            builder.Property(s => s.HireDate).IsRequired();

            // -----------------
            // Relationships (Staff is dependent)
            // -----------------

            // Staff → Person (1-to-1)
            builder.HasOne(s => s.Person)
                   .WithOne() // Person does not hold reference
                   .HasForeignKey<Staff>(s => s.PersonId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Staff → Branch (many-to-one)
            builder.HasOne(s => s.Branch)
                   .WithMany(b => b.Staffs)
                   .HasForeignKey(s => s.BranchId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
