#region Usings
using LibraryManagement.Domain.Interfaces;
using LibraryManagementSystem.Application.Features.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
#endregion
namespace LibraryManagementSystem.Application.Features.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> ReserveBookAsync(string bookTitle, string branchName, string username)
        {
            var user = await GetUserAsync(username);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var branch = await GetBranchAsync(branchName);
                var book = await GetBookAsync(bookTitle);

                var copy = GetAvailableCopy(book, branch.Id);

                var reservation = CreateReservation(user.Id, copy.Id);

                await _unitOfWork.Reservations.AddAsync(reservation);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return $"Book '{bookTitle}' reserved successfully for user '{username}'.";
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        #region Helper Methods
        private async Task<User> GetUserAsync(string username)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
                throw new InvalidOperationException("User does not exist.");
            return user;
        }

        private async Task<Branch> GetBranchAsync(string branchName)
        {
            var branch = await _unitOfWork.Branches.FirstOrDefaultAsync(b => b.Name == branchName);
            if (branch == null)
                throw new InvalidOperationException("Branch does not exist.");
            return branch;
        }

        private async Task<BookTitle> GetBookAsync(string bookTitle)
        {
            var book = await _unitOfWork.BookTitles
                .FirstOrDefaultAsync(bt => bt.Title == bookTitle);
            if (book == null)
                throw new InvalidOperationException("Book title does not exist.");
            return book;
        }

        private BookCopy GetAvailableCopy(BookTitle book, Guid branchId)
        {
            var copy = book.BookCopies
                .FirstOrDefault(c => c.BranchId == branchId && c.CopyStatus == BookCopyStatus.Available);
            if (copy == null)
                throw new InvalidOperationException("No available copies in this branch.");
            return copy;
        }

        private Reservation CreateReservation(Guid userId, Guid copyId)
        {
            return new Reservation
            {
                UserId = userId,
                BookCopyId = copyId,
                ReservationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                Status = ReservationStatus.Active
            };
        }

    }

    #endregion

}
