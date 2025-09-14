using LibraryManagementSystem.Application.DTOs.Request;
using LibraryManagementSystem.Application.DTOs.Security;
using LibraryManagementSystem.Application.Features.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Controllers.Authentication
{
    [ApiController]
    [Route("api/v1/auth/[controller]")]
    [AllowAnonymous] // Public endpoint
    [Produces("application/json")]
    public class RegistrationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public RegistrationController(IAuthenticationService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        /// <summary>
        /// Registers a new user with person, address, contact, residency, and nationality details.
        /// </summary>
        /// <param name="request">Full registration request DTO.</param>
        /// <returns>Confirmation message.</returns>
        /// <response code="201">User registered successfully.</response>
        /// <response code="400">Invalid request or missing required data.</response>
        /// <response code="409">User already exists.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] FullRegistrationDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid registration data.", Details = ModelState });

            try
            {
                await _authService.RegisterAsync(request);
                return StatusCode(StatusCodes.Status201Created, new { Message = "User registered successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Error = ex.Message }); // e.g. duplicate user/email
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Error = "An unexpected error occurred during registration.",
                    Details = ex.Message
                });
            }
        }

    }
}
