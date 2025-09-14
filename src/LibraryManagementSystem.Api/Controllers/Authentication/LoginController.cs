using LibraryManagementSystem.Application.DTOs.Request;
using LibraryManagementSystem.Application.DTOs.Security;
using LibraryManagementSystem.Application.Features.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Controllers.Authentication
{
    [ApiController]
    [Route("api/v1/auth/[controller]")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public LoginController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token with roles and permissions.
        /// </summary>
        /// <param name="request">Login request DTO containing username/email and password.</param>
        /// <returns>JWT token, expiration, roles, and permissions.</returns>
        /// <response code="200">Login successful, returns token, roles, and permissions.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="401">Invalid credentials.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            if (request == null)
                return BadRequest(new { Error = "Request body is required." });

            try
            {
                var authResponse = await _authService.LoginAsync(request);
                return Ok(authResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Error = "An unexpected error occurred.",
                    Details = ex.Message
                });
            }
        }
    }
}
