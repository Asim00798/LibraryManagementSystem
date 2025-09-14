using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOs.Request
{
    /// <summary>
    /// DTO for reserving a book.
    /// </summary>
    public class ReserveBookRequestDto
    {
        /// <summary>
        /// Title of the book to reserve.
        /// </summary>
        public string BookTitle { get; set; } = string.Empty;

        /// <summary>
        /// Branch name where the book should be reserved.
        /// </summary>
        public string BranchName { get; set; } = string.Empty;

        /// <summary>
        /// Username of the user making the reservation.
        /// </summary>
        public string Username { get; set; } = string.Empty;
    }
}
