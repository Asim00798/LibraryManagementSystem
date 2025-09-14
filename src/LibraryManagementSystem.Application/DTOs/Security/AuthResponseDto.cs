using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Application.DTOs.Security
{
    public class AuthResponseDto
    {
        /// <summary>
        /// JWT token string.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Token expiration timestamp.
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Roles assigned to the authenticated user.
        /// </summary>
        public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Permissions assigned to the authenticated user.
        /// </summary>
        public IEnumerable<string> Permissions { get; set; } = Array.Empty<string>();
    }
}
