using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations
{
    public class AuthorConfiguration : BaseEntityConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);

            // Primary Key
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                   .ValueGeneratedOnAdd();

            // Properties
            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(a => a.AuthorDetailId);

            // Relations (dependent side only)
            builder.HasOne(a => a.AuthorDetail)
                   .WithOne(ad => ad.Author)
                   .HasForeignKey<Author>(a => a.AuthorDetailId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(a => a.BookAuthors)
                   .WithOne(ba => ba.Author)
                   .HasForeignKey(ba => ba.AuthorId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(a => a.Name);
        }
    }
}
