using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Features.Interfaces
{
    public interface IBorrowingService
    {
        Task<string> BorrowBook(string bookTitle, string branchName, string username);
        Task<IEnumerable<Borrowing>> GetUserBorrowings(string username);
        Task<IEnumerable<Borrowing>> GetOverdueBorrowings();
        Task<string> ReturnBook(string bookCopyBarCode, string username);
    }
}
