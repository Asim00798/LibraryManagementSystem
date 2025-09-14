#region Usings
using LibraryManagement.Domain.Entities.Security;
using LibraryManagement.Domain.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Security;
using LibraryManagementSystem.Infrastructure.Persistence;
using LibraryManagementSystem.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
#endregion
namespace LibraryManagementSystem.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _DbContext;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(LibraryDbContext context)
        {
            _DbContext = context;
            #region Repos Initialization
            Addresses = new BaseRepository<Address, Guid>(_DbContext);
            AuditLogs = new BaseRepository<AuditLog, Guid>(_DbContext);
            Authors = new BaseRepository<Author, int>(_DbContext);
            AuthorDetails = new BaseRepository<AuthorDetail, int>(_DbContext);
            BookAuthors = new BaseRepository<BookAuthor, int>(_DbContext);
            BookCopies = new BaseRepository<BookCopy, Guid>(_DbContext);
            BookTitles = new BaseRepository<BookTitle, int>(_DbContext);
            Borrowings = new BaseRepository<Borrowing, Guid>(_DbContext);
            Branches = new BaseRepository<Branch, Guid>(_DbContext);
            Categories = new BaseRepository<Category, int>(_DbContext);
            Contacts = new BaseRepository<Contact, Guid>(_DbContext);
            Countries = new BaseRepository<Country, Guid>(_DbContext);
            Events = new BaseRepository<Event, Guid>(_DbContext);
            Languages = new BaseRepository<Language, int>(_DbContext);
            Memberships = new BaseRepository<Membership, Guid>(_DbContext);
            Nationalities = new BaseRepository<Nationality, Guid>(_DbContext);
            Notifications = new BaseRepository<Notification, Guid>(_DbContext);
            PaymentLogs = new BaseRepository<PaymentLog, Guid>(_DbContext);
            People = new BaseRepository<Person, Guid>(_DbContext);
            Publishers = new BaseRepository<Publisher, Guid>(_DbContext);
            Registrations = new BaseRepository<Registration, Guid>(_DbContext);
            Reservations = new BaseRepository<Reservation, Guid>(_DbContext);
            Residencies = new BaseRepository<Residency, Guid>(_DbContext);
            Reviews = new BaseRepository<Review, Guid>(_DbContext);
            Shelves = new BaseRepository<Shelf, Guid>(_DbContext);
            Staffs = new BaseRepository<Staff, Guid>(_DbContext);
            StaffAttendances = new BaseRepository<StaffAttendance, Guid>(_DbContext);
            Subscriptions = new BaseRepository<Subscription, Guid>(_DbContext);
            Users = new BaseRepository<User, Guid>(_DbContext);
            Permissions = new BaseRepository<Permission, Guid>(_DbContext);
            RolePermissions = new BaseRepository<RolePermission, Guid>(_DbContext);
            Roles = new BaseRepository<Role, Guid>(_DbContext);
            UserRoles = new BaseRepository<IdentityUserRole<Guid>, Guid>(_DbContext);
            #endregion
        }

        #region Repos
        public IBaseRepository<Address, Guid> Addresses { get; }
        public IBaseRepository<AuditLog, Guid> AuditLogs { get; }
        public IBaseRepository<Author, int> Authors { get; }
        public IBaseRepository<AuthorDetail, int> AuthorDetails { get; }
        public IBaseRepository<BookAuthor, int> BookAuthors { get; }
        public IBaseRepository<BookCopy, Guid> BookCopies { get; }
        public IBaseRepository<BookTitle, int> BookTitles { get; }
        public IBaseRepository<Borrowing, Guid> Borrowings { get; }
        public IBaseRepository<Branch, Guid> Branches { get; }
        public IBaseRepository<Category, int> Categories { get; }
        public IBaseRepository<Contact, Guid> Contacts { get; }
        public IBaseRepository<Country, Guid> Countries { get; }
        public IBaseRepository<Event, Guid> Events { get; }
        public IBaseRepository<Language, int> Languages { get; }
        public IBaseRepository<Membership, Guid> Memberships { get; }
        public IBaseRepository<Nationality, Guid> Nationalities { get; }
        public IBaseRepository<Notification, Guid> Notifications { get; }
        public IBaseRepository<PaymentLog, Guid> PaymentLogs { get; }
        public IBaseRepository<Person, Guid> People { get; }
        public IBaseRepository<Publisher, Guid> Publishers { get; }
        public IBaseRepository<Registration, Guid> Registrations { get; }
        public IBaseRepository<Reservation, Guid> Reservations { get; }
        public IBaseRepository<Residency, Guid> Residencies { get; }
        public IBaseRepository<Review, Guid> Reviews { get; }
        public IBaseRepository<Shelf, Guid> Shelves { get; }
        public IBaseRepository<Staff, Guid> Staffs { get; }
        public IBaseRepository<StaffAttendance, Guid> StaffAttendances { get; }
        public IBaseRepository<Subscription, Guid> Subscriptions { get; }
        public IBaseRepository<User, Guid> Users { get; }
        public IBaseRepository<Permission, Guid> Permissions { get; }
        public IBaseRepository<RolePermission, Guid> RolePermissions { get; }
        public IBaseRepository<Role, Guid> Roles { get; }
        public IBaseRepository<IdentityUserRole<Guid>,Guid> UserRoles { get; } // built-in join table
        #endregion

        #region Transactions

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
                _transaction = await _DbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _DbContext.SaveChangesAsync();
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        #endregion

        #region SaveChanges / Complete

        public async Task<int> SaveChangesAsync() => await _DbContext.SaveChangesAsync();
        public int SaveChanges() => _DbContext.SaveChanges();

        public async Task<int> CompleteAsync() => await SaveChangesAsync();
        public int Complete() => SaveChanges();

        #endregion

        public void Dispose()
        {
            _transaction?.Dispose();
            _DbContext.Dispose();
        }

    }
}
