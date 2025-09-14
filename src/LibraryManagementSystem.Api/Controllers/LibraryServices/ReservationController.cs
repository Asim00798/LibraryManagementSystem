using LibraryManagementSystem.Application.DTOs.Request;
using LibraryManagementSystem.Application.Features.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Controllers.LibraryServices
{
    [ApiController]
    [Route("api/v1/library-services/[controller]")]
    [Produces("application/json")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
        }

        /// <summary>
        /// Reserve a book for a user at a specific branch.
        /// </summary>
        /// <param name="request">Contains BookTitle, BranchName, and Username.</param>
        /// <returns>Reservation success message.</returns>
        [HttpPost("reserve")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReserveBook([FromBody] ReserveBookRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid request data.", Details = ModelState });

            try
            {
                var result = await _reservationService.ReserveBookAsync(request.BookTitle, request.BranchName, request.Username);
                return Ok(new { Message = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to reserve the book.", Details = ex.Message });
            }
        }
    }
}
