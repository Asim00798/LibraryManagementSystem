using LibraryManagementSystem.Application.DTOs.Security;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Features.Interfaces
{
    public interface IProfileService
    {
        /// <summary>
        /// Show greetings to user
        /// </summary>
        Task<string> Me();

        /// <summary>
        /// Update user profile with provided details.
        /// </summary>
        Task<string> UpdateProfileAsync(UpdateProfileDto updateProfileDto);

        /// <summary>
        /// Change the personal image of the user.
        /// </summary>
        Task<string> ChangePersonalImageAsync(string imageUrl);

        /// <summary>
        /// Delete the personal image of the user.
        /// </summary>
        Task<string> DeletePersonalImageAsync();

        /// <summary>
        /// Change the password of the user.
        /// </summary>
        Task<string> ChangePasswordAsync(string currentPassword, string newPassword);
    }
}
