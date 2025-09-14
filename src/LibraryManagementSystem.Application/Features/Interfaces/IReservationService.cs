using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Features.Interfaces
{
    public interface IReservationService
    {
        /// <summary>
        /// Reserve a book copy for a user in a specific branch.
        /// </summary>
        /// <param name="bookTitle">Book title</param>
        /// <param name="branchName">Branch name</param>
        /// <param name="username">User's username</param>
        /// <returns>Result message</returns>
        Task<string> ReserveBookAsync(string bookTitle, string branchName, string username);
    }
}
