using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class ReviewConfiguration : BaseEntityConfiguration<Review>
    {
        public override void Configure(EntityTypeBuilder<Review> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(r => r.Comment).IsRequired();
            builder.Property(r => r.ReviewRate).IsRequired();

            // -----------------
            // Relationships (dependent = Review)
            // -----------------

            // Review → User (optional, one-to-many)
            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reviews)  
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Review → BookTitle (optional, one-to-many)
            builder.HasOne(r => r.BookTitle)
                   .WithMany(bt => bt.Reviews)  
                   .HasForeignKey(r => r.BookTitleId)
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
