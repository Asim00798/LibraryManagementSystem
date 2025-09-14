using LibraryManagementSystem.Application.DTOs.Request;
using LibraryManagementSystem.Application.DTOs.Security;
using LibraryManagementSystem.Application.Features.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Controllers.Authentication
{
    [ApiController]
    [Route("api/v1/auth/[controller]")]
    [Produces("application/json")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// Get current authenticated user's basic info.
        /// </summary>
        /// <returns>Greeting message with username.</returns>
        [HttpGet("me")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 401)]
        [ProducesResponseType(typeof(object), 500)]
        public async Task<IActionResult> Me()
        {
            try
            {
                var result = await _profileService.Me();
                return Ok(new { Message = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to retrieve profile.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Update profile information including person, address, contact, residency, and nationality.
        /// </summary>
        [HttpPut("update")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(object), 401)]
        [ProducesResponseType(typeof(object), 500)]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto request)
        {
            try
            {
                var result = await _profileService.UpdateProfileAsync(request);
                return Ok(new { Message = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to update profile.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Change the personal profile image.
        /// </summary>
        [HttpPut("image/change")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(object), 401)]
        [ProducesResponseType(typeof(object), 500)]
        public async Task<IActionResult> ChangeImage([FromBody] ChangeImageRequestDto request)
        {
            try
            {
                var result = await _profileService.ChangePersonalImageAsync(request.ImageUrl);
                return Ok(new { Message = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to change personal image.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Delete the personal profile image.
        /// </summary>
        [HttpDelete("image/delete")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 401)]
        [ProducesResponseType(typeof(object), 500)]
        public async Task<IActionResult> DeleteImage()
        {
            try
            {
                var result = await _profileService.DeletePersonalImageAsync();
                return Ok(new { Message = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to delete personal image.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Change the user's password.
        /// </summary>
        [HttpPut("password/change")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(object), 401)]
        [ProducesResponseType(typeof(object), 500)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            try
            {
                var result = await _profileService.ChangePasswordAsync(request.CurrentPassword, request.NewPassword);
                return Ok(new { Message = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to change password.", Details = ex.Message });
            }
        }
    }
}
