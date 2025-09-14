using LibraryManagementSystem.Application.DTOs.Security;
using LibraryManagementSystem.Application.Features.Interfaces;
using LibraryManagementSystem.Domain.Entities.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Controllers.Admin.ManageUsers
{
    [ApiController]
    [Route("api/v1/admin/ManageUsers/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// Get all permissions assigned to a specific role.
        /// </summary>
        [HttpGet("role/{roleName}")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPermissionsByRole(string roleName)
        {
            try
            {
                var permissions = await _permissionService.GetPermissionsByRoleAsync(roleName);
                if (permissions == null) return NotFound(new { Error = $"Role '{roleName}' not found." });

                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to retrieve permissions.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Assign a permission to a role.
        /// </summary>
        [HttpPost("assign")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignPermission([FromBody] AssignPermissionRequestDto request)
        {
            try
            {
                var result = await _permissionService.AssignPermissionToRoleAsync(request.RoleName, request.PermissionName);
                if (result.Contains("not found")) return NotFound(new { Error = result });
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to assign permission.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Remove a permission from a role.
        /// </summary>
        [HttpPost("remove")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemovePermission([FromBody] AssignPermissionRequestDto request)
        {
            try
            {
                var result = await _permissionService.RemovePermissionFromRoleAsync(request.RoleName, request.PermissionName);
                if (result.Contains("not assigned") || result.Contains("not found")) return NotFound(new { Error = result });
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to remove permission.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Get all permissions assigned to a specific user.
        /// </summary>
        [HttpGet("user/{userId:guid}")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserPermissions(Guid userId)
        {
            try
            {
                var permissions = await _permissionService.GetUserPermissionsAsync(new User { Id = userId });
                if (permissions == null) return NotFound(new { Error = "User not found or no permissions assigned." });

                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to retrieve user permissions.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Check if a user has a specific permission.
        /// </summary>
        [HttpGet("check/{userId:guid}/{permissionName}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UserHasPermission(Guid userId, string permissionName)
        {
            try
            {
                var hasPermission = await _permissionService.UserHasPermissionAsync(userId, permissionName);
                return Ok(new { HasPermission = hasPermission });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to check permission.", Details = ex.Message });
            }
        }
    }
}
