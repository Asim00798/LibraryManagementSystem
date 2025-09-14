using LibraryManagementSystem.Application.DTOs.Request;
using LibraryManagementSystem.Application.Features.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Controllers.Admin.Custom
{
    /// <summary>
    /// Controller to manage system languages.
    /// Supports role-based and permission-based access.
    /// </summary>
    [ApiController]
    [Route("api/v1/admin/custom/[controller]")]
    [Authorize(Roles = "Admin")] // all actions require admin by default
    public class LanguagesController : ControllerBase
    {
        private readonly IManageLanguagesService _languageService;

        public LanguagesController(IManageLanguagesService languageService)
        {
            _languageService = languageService;
        }

        /// <summary>
        /// Retrieves all available languages.
        /// </summary>
        [HttpGet("GetAllLanguages")]
        [Authorize(Policy = "Permission:ViewLanguages")]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetLanguages()
        {
            var languages = await _languageService.GetLanguagesAsync();
            return Ok(languages);
        }

        /// <summary>
        /// Adds a new language.
        /// </summary>
        [HttpPost("AddLanguage")]
        [Authorize(Policy = "Permission:ManageLanguages")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddLanguage([FromBody] AddLanguageRequest request)
        {
            await _languageService.AddLanguageAsync(request.Name);
            return Created("", new { Message = $"Language '{request.Name}' added successfully." });
        }

        /// <summary>
        /// Removes an existing language.
        /// </summary>
        [HttpDelete("RemoveLanguage")]
        [Authorize(Policy = "Permission:ManageLanguages")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> RemoveLanguage(string name)
        {
            await _languageService.RemoveLanguageAsync(name);
            return Ok(new { Message = $"Language '{name}' removed successfully." });
        }

        /// <summary>
        /// Updates an existing language name.
        /// </summary>
        [HttpPut("UpdateLanguage")]
        [Authorize(Policy = "Permission:ManageLanguages")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateLanguage(string name, [FromBody] UpdateLanguageRequest request)
        {
            await _languageService.UpdateLanguageAsync(name, request.NewName);
            return Ok(new { Message = $"Language '{name}' updated to '{request.NewName}' successfully." });
        }
    }
}
