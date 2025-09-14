#region Usings
using LibraryManagement.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Interfaces.Security;
using LibraryManagementSystem.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Design;
using LibraryManagementSystem.Infrastructure.Persistence.DataSeed.Migration;
#endregion

namespace LibraryManagementSystem.Infrastructure.Persistence.Context
{
    public class LibraryDbContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly ICurrentUser? _CurrentUser;

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options, ICurrentUser? currentUser)
            : base(options)
        {
            _CurrentUser = currentUser;
        }

        //==========================
        // DbSets For All Entities  
        //==========================

        #region Entities

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorDetail> AuthorDetails { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<BookTitle> BookTitles { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PaymentLog> PaymentLogs { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Residency> Residencies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffAttendance> StaffAttendances { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        //Security DbSets
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        #endregion

        //============================
        // Model Configuration
        //============================

        #region Model Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply IEntityTypeConfiguration<T>
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Apply soft-delete filters for all BaseEntity-derived entities
            modelBuilder.ApplySoftDeleteFilter();

            // Rename Identity Tables
            modelBuilder.RenameIdentityTables();

            //Seed Data
            Seed.SeedAll(modelBuilder);
        }
        #endregion

        //============================
        // Save Changes Overrides
        //============================

        #region Overrides

        /// <summary>
        /// Applies auditing and persistence logic before saving changes.
        /// </summary>

        public override int SaveChanges()
        {
            Guid? currentUserId = _CurrentUser?.UserId;
            this.ApplyPersistenceLogic(currentUserId);
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Guid? currentUserId = _CurrentUser?.UserId;
            this.ApplyPersistenceLogic(currentUserId);
            return await base.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
