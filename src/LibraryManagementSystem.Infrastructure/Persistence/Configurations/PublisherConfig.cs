using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class PublisherConfiguration : BaseEntityConfiguration<Publisher>
    {
        public override void Configure(EntityTypeBuilder<Publisher> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Properties
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.AddressId).IsRequired(false);

            // -----------------
            // Relationships (only on dependent side)
            // -----------------

            // BookTitle -> Publisher (dependent side)
            builder.HasMany(p => p.BookTitles)
                   .WithOne(bt => bt.Publisher)
                   .HasForeignKey(bt => bt.PublisherId)
                   .OnDelete(DeleteBehavior.Restrict);

            // BookCopy -> Publisher (dependent side)
            builder.HasMany(p => p.BookCopies)
                   .WithOne(bc => bc.Publisher)
                   .HasForeignKey(bc => bc.PublisherId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Publisher -> Address (optional, many-to-one)
            builder.HasOne(p => p.Address)
                   .WithMany()
                   .HasForeignKey(p => p.AddressId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
