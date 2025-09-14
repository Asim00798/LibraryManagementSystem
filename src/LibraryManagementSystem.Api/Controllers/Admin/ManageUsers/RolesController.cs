using LibraryManagementSystem.Application.DTOs.Request;
using LibraryManagementSystem.Application.Features.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Controllers.Admin.ManageUsers
{
    [ApiController]
    [Route("api/v1/admin/ManageUsers/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Get all available roles.
        /// </summary>
        [HttpGet("all")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Get all roles assigned to a specific user.
        /// </summary>
        [HttpGet("user/{username}")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRolesByUser(string username)
        {
            try
            {
                var roles = await _roleService.GetRolesByUserAsync(username);
                return Ok(roles);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Assign a role to a user.
        /// </summary>
        [HttpPost("assign")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequestDto request)
        {
            var result = await _roleService.AssignRoleToUserAsync(request.Username, request.RoleName);
            if (result.Contains("not found"))
                return NotFound(new { Error = result });

            return Ok(new { Message = result });
        }

        /// <summary>
        /// Remove a role from a user.
        /// </summary>
        [HttpPost("remove")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveRole([FromBody] AssignRoleRequestDto request)
        {
            var result = await _roleService.RemoveRoleFromUserAsync(request.Username, request.RoleName);
            if (result.Contains("not found"))
                return NotFound(new { Error = result });

            return Ok(new { Message = result });
        }

        /// <summary>
        /// Create a new role.
        /// </summary>
        [HttpPost("create")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequestDto request)
        {
            var result = await _roleService.CreateRoleAsync(request.RoleName);
            if (result.Contains("already exists"))
                return BadRequest(new { Error = result });

            return Ok(new { Message = result });
        }

        /// <summary>
        /// Delete an existing role.
        /// </summary>
        [HttpDelete("delete/{roleName}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var result = await _roleService.DeleteRoleAsync(roleName);
            if (result.Contains("not found"))
                return NotFound(new { Error = result });

            return Ok(new { Message = result });
        }
    }
}
