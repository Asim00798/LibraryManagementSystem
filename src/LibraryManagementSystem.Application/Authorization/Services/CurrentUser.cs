using LibraryManagementSystem.Domain.Interfaces.Security;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace LibraryManagementSystem.Application.Authorization.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public Guid? UserId =>
            Guid.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
                ? userId
                : (Guid?)null;

        public string? UserName => User?.Identity?.Name;

        public string? Email => User?.FindFirstValue(ClaimTypes.Email);

        public IEnumerable<string> Roles =>
            User?.FindAll(ClaimTypes.Role).Select(r => r.Value) ?? Enumerable.Empty<string>();

        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
    }
}

