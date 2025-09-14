using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Features.Interfaces
{
    public interface IManageLanguagesService
    {
        Task<List<string>> GetLanguagesAsync();          // Return list of languages
        Task AddLanguageAsync(string Name);              // Add a language
        Task RemoveLanguageAsync(string Name);           // Remove a language
        Task UpdateLanguageAsync(string Name, string NewName);  // Update a language
    }
}
