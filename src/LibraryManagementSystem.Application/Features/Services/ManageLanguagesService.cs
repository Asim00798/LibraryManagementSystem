#region Usings
using LibraryManagement.Domain.Interfaces;
using LibraryManagementSystem.Application.Features.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace LibraryManagementSystem.Application.Features.Services
{
    public class ManageLanguagesService : IManageLanguagesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManageLanguagesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<string>> GetLanguagesAsync()
        {
            var languages = await _unitOfWork.Languages.GetAllAsync();
            return languages.Select(l => l.Name).ToList();
        }

        public async Task AddLanguageAsync(string name)
        {
            var exists = await _unitOfWork.Languages.AnyAsync(l => l.Name == name);
            if (exists) return; // Or throw exception if needed

            var language = new Language { Name = name };
            await _unitOfWork.Languages.AddAsync(language);
            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveLanguageAsync(string name)
        {
            var language = await _unitOfWork.Languages.FirstOrDefaultAsync(l => l.Name == name);
            if (language == null) return; // Or throw exception

            await _unitOfWork.Languages.RemoveAsync(language);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateLanguageAsync(string name, string newName)
        {
            // Find the language by current name
            var language = await _unitOfWork.Languages.FirstOrDefaultAsync(l => l.Name == name);

            if (language == null)
                throw new InvalidOperationException("Language name not found");
            
            // Optional: check if new name already exists
            var exists = await _unitOfWork.Languages.AnyAsync(l => l.Name == newName);
            if (exists) return; // Or throw exception

            language.Name = newName;
            await _unitOfWork.Languages.UpdateAsync(language);
            await _unitOfWork.CompleteAsync();
        }
    }
}
