#region Usings
using LibraryManagement.Domain.Interfaces;
using LibraryManagementSystem.Application.Features.Interfaces;
using LibraryManagementSystem.Domain.Constants;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace LibraryManagementSystem.Application.Features.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BorrowingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> BorrowBook(string bookTitle, string branchName, string username)
        {
            var user = await GetUserAsync(username);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var branch = await GetBranchAsync(branchName);
                var book = await GetBookAsync(bookTitle);
                var copy = GetAvailableCopy(book, branch.Id);

                var reservation = await GetUserReservationAsync(user.Id, copy.Id);

                var borrowing = CreateBorrowing(user.Id, copy.Id);

                if (reservation != null)
                {
                    borrowing.ReservationId = reservation.Id;
                    reservation.Status = ReservationStatus.Fulfilled;
                }

                AddPaymentLog(borrowing);
                copy.CopyStatus = BookCopyStatus.Borrowed;

                await _unitOfWork.Borrowings.AddAsync(borrowing);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return $"Book '{bookTitle}' borrowed successfully by '{username}'.";
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        public async Task<string> ReturnBook(string bookCopyBarCode, string username)
        {
            // Ensure user exists
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
                throw new InvalidOperationException("User does not exist.");

            // Find the book copy by barcode
            var copy = await _unitOfWork.BookCopies.FirstOrDefaultAsync(c => c.Barcode == bookCopyBarCode);
            if (copy == null)
                throw new InvalidOperationException("Book copy not found.");

            // Find the active borrowing for this user and copy
            var borrowing = await _unitOfWork.Borrowings
                .Query()
                .Where(b => b.BookCopyId == copy.Id && b.UserId == user.Id && b.Status == BorrowingStatus.Borrowed)
                .FirstOrDefaultAsync();

            if (borrowing == null)
                throw new InvalidOperationException("No active borrowing found for this book and user.");

            // Update borrowing status
            borrowing.Status = BorrowingStatus.Returned;
            borrowing.ReturnedAt = DateTime.UtcNow;

            // Update book copy status
            copy.CopyStatus = BookCopyStatus.Available;

            // Save changes
            await _unitOfWork.CompleteAsync();

            return $"Book with barcode '{bookCopyBarCode}' returned successfully by '{username}'.";
        }
        public async Task<IEnumerable<Borrowing>> GetUserBorrowings(string username)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) throw new InvalidOperationException("User does not exist.");

            var borrowings = await _unitOfWork.Borrowings
                .Query()
                .Where(b => b.UserId == user.Id)
                .ToListAsync();

            return borrowings;
        }
        public async Task<IEnumerable<Borrowing>> GetOverdueBorrowings()
        {
            var now = DateTime.UtcNow;
            var overdue = await _unitOfWork.Borrowings
                .Query()
                .Where(b => b.DueAt.HasValue && b.DueAt < now && b.Status == BorrowingStatus.Borrowed)
                .ToListAsync();

            return overdue;
        }

        #region PrivateHelpers
        private async Task<User> GetUserAsync(string username)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) throw new InvalidOperationException("User does not exist.");
            return user;
        }

        private async Task<Branch> GetBranchAsync(string branchName)
        {
            var branch = await _unitOfWork.Branches.FirstOrDefaultAsync(b => b.Name == branchName);
            if (branch == null) throw new InvalidOperationException("Branch does not exist.");
            return branch;
        }

        private async Task<BookTitle> GetBookAsync(string bookTitle)
        {
            var book = await _unitOfWork.BookTitles.FirstOrDefaultAsync(bt => bt.Title == bookTitle);
            if (book == null) throw new InvalidOperationException("Book title does not exist.");
            return book;
        }

        private BookCopy GetAvailableCopy(BookTitle book, Guid branchId)
        {
            var copy = book.BookCopies.FirstOrDefault(c => c.BranchId == branchId && c.CopyStatus == BookCopyStatus.Available);
            if (copy == null) throw new InvalidOperationException("No available copies in this branch.");
            return copy;
        }

        private async Task<Reservation?> GetUserReservationAsync(Guid userId, Guid copyId)
        {
            return await _unitOfWork.Reservations
                .FirstOrDefaultAsync(r => r.UserId == userId && r.BookCopyId == copyId && r.Status == ReservationStatus.Active);
        }

        private Borrowing CreateBorrowing(Guid userId, Guid copyId)
        {
            return new Borrowing
            {
                UserId = userId,
                BookCopyId = copyId,
                BorrowedAt = DateTime.UtcNow,
                Status = BorrowingStatus.Borrowed,
                PricePerDay = BorrowingConstants.PricePerDay
            };
        }

        private void AddPaymentLog(Borrowing borrowing)
        {
            var paymentLog = new PaymentLog
            {
                Amount = borrowing.PricePerDay,
                PaymentDate = DateTime.UtcNow,
                PaymentType = PaymentType.Borrowing,
                PaymentMethod = PaymentMethod.Cash,
                PaymentStatus = PaymentStatus.Completed,
                Currency = Currency.AED
            };
            borrowing.PaymentLogs.Add(paymentLog);
        }
    }

    #endregion
}
