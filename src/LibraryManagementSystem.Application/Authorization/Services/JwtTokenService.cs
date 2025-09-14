using LibraryManagementSystem.Application.Authorization.Configuration;
using LibraryManagementSystem.Application.Authorization.Interfaces;
using LibraryManagementSystem.Application.DTOs.Security;
using LibraryManagementSystem.Application.Features.Interfaces;
using LibraryManagementSystem.Domain.Entities.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementSystem.Application.Authorization.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;

        public JwtTokenService(
            IOptions<JwtSettings> jwtOptions,
            UserManager<User> userManager,
            IRoleService roleService,
            IPermissionService permissionService)
        {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
            _roleService = roleService;
            _permissionService = permissionService;
        }

        public async Task<AuthResponseDto> GenerateTokenAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("RegistrationId", user.RegistrationId.ToString())
            };

            // ---------------------------
            // Add Roles
            // ---------------------------
            var roles = await _userManager.GetRolesAsync(user); // Identity roles
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            // ---------------------------
            // Add Permissions
            // ---------------------------
            var permissions = await _permissionService.GetUserPermissionsAsync(user);
            claims.AddRange(permissions.Select(p => new Claim("permission", p)));

            // ---------------------------
            // Build Token
            // ---------------------------
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: creds
            );

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Roles = roles,
                Permissions = permissions
            };
        }
    }
}
