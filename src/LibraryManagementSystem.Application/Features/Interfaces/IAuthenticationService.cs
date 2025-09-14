using LibraryManagementSystem.Application.DTOs.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Features.Interfaces
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Registers a new user with the provided details.
        /// Throws an exception if registration fails.
        /// </summary>
        Task RegisterAsync(FullRegistrationDto request);

        /// <summary>
        /// Authenticates a user and returns a JWT token if successful.
        /// Throws UnauthorizedAccessException if credentials are invalid.
        /// </summary>
        Task<AuthResponseDto> LoginAsync(LoginDto request);
    }
}
