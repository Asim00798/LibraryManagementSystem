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
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
        }

        /// <summary>
        /// Create a new subscription for a user.
        /// </summary>
        /// <param name="request">Subscription details including user, membership, branch, and payment information.</param>
        /// <response code="200">Subscription created successfully.</response>
        /// <response code="400">Invalid request data or business rule violation.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("subscribe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Subscribe([FromBody] SubscriptionRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Error = "Invalid request data.", Details = ModelState });

            try
            {
                var result = await _subscriptionService.SubscribeAsync(request);
                return Ok(new { Message = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to create subscription.", Details = ex.Message });
            }
        }
    }
}
