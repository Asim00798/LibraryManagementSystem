using LibraryManagementSystem.Application.Authorization.Interfaces;
using LibraryManagementSystem.Application.Features.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Authorization.Services
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService;
        private readonly ILogger<PermissionHandler> _logger;

        public PermissionHandler(IPermissionService permissionService, ILogger<PermissionHandler> logger)
        {
            _permissionService = permissionService;
            _logger = logger;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.Identity?.IsAuthenticated != true)
            {
                context.Fail();
                return;
            }

            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                _logger.LogWarning("PermissionHandler: Invalid or missing NameIdentifier claim.");
                context.Fail();
                return;
            }

            // Admin bypass
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return;
            }

            var hasPermission = await _permissionService.UserHasPermissionAsync(userId, requirement.PermissionName);
            if (hasPermission)
            {
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogWarning("PermissionHandler: User {UserId} lacks permission {Permission}.", userId, requirement.PermissionName);
                context.Fail();
            }
        }
    }
}
