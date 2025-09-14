using LibraryManagementSystem.Application.DTOs.Request;
using LibraryManagementSystem.Application.Features.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Controllers.LibraryServices
{
    [ApiController]
    [Route("api/v1/library-services/[controller]")]
    [Produces("application/json")]
    public class BorrowingsController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;

        public BorrowingsController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService ?? throw new ArgumentNullException(nameof(borrowingService));
        }

        /// <summary>
        /// Borrow a book for a user.
        /// </summary>
        /// <param name="request">The borrowing request containing book title, branch, and username.</param>
        /// <returns>Message indicating the result of the borrowing operation.</returns>
        [HttpPost("borrow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowBookRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid request.", Details = ModelState });

            try
            {
                var result = await _borrowingService.BorrowBook(
                    request.BookTitle,
                    request.BranchName,
                    request.Username);

                return Ok(new { Message = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An unexpected error occurred while borrowing.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Return a borrowed book.
        /// </summary>
        /// <param name="request">The return request containing the book copy barcode and username.</param>
        /// <returns>Message indicating the result of the return operation.</returns>
        [HttpPost("return")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid request.", Details = ModelState });

            try
            {
                var result = await _borrowingService.ReturnBook(request.Barcode, request.Username);
                return Ok(new { Message = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An unexpected error occurred while returning.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get all borrowings for a specific user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>List of borrowings for the user.</returns>
        [HttpGet("user/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserBorrowings(string username)
        {
            try
            {
                var borrowings = await _borrowingService.GetUserBorrowings(username);
                return Ok(borrowings);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Get all overdue borrowings.
        /// </summary>
        /// <returns>List of overdue borrowings.</returns>
        [HttpGet("overdue")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOverdueBorrowings()
        {
            var overdue = await _borrowingService.GetOverdueBorrowings();
            return Ok(overdue);
        }
    }
}
