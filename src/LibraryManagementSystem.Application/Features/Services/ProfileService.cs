#region Usings
using LibraryManagementSystem.Application.DTOs.Request;
using LibraryManagementSystem.Application.DTOs.Security;
using LibraryManagementSystem.Application.Features.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Interfaces.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
#endregion
namespace LibraryManagementSystem.Application.Features.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUser _currentUser;

        public ProfileService(UserManager<User> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task<string> Me()
        {
            var user = await GetCurrentUserAsync();
            return $"Hello {user.UserName} :)";
        }

        public async Task<string> UpdateProfileAsync(UpdateProfileDto updateProfileDto)
        {
            var user = await GetCurrentUserAsync();
            var person = user.Registration.Person;

            // Update each entity using helpers
            if (updateProfileDto.Person != null)
                UpdatePerson(person, updateProfileDto.Person);

            if (updateProfileDto.Address != null)
                UpdateAddress(person, updateProfileDto.Address);

            if (updateProfileDto.Contact != null)
                UpdateContact(person, updateProfileDto.Contact);

            if (updateProfileDto.Residency != null)
                UpdateResidency(person, updateProfileDto.Residency);

            if (updateProfileDto.Nationality != null)
                UpdateNationality(person, updateProfileDto.Nationality);

            await _userManager.UpdateAsync(user);

            return "Profile updated successfully.";
        }

        public async Task<string> ChangePersonalImageAsync(string imageUrl)
        {
            var user = await GetCurrentUserAsync();
            user.Registration.Person.ProfileImageUrl = imageUrl;
            await _userManager.UpdateAsync(user);
            return "Personal image updated successfully.";
        }

        public async Task<string> DeletePersonalImageAsync()
        {
            var user = await GetCurrentUserAsync();
            user.Registration.Person.ProfileImageUrl = null;
            await _userManager.UpdateAsync(user);
            return "Personal image deleted successfully.";
        }

        public async Task<string> ChangePasswordAsync(string currentPassword, string newPassword)
        {
            var user = await GetCurrentUserAsync();
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join("; ", result.Errors));

            return "Password changed successfully.";
        }

        #region Private Helpers
        private async Task<User> GetCurrentUserAsync()
        {
            if (!_currentUser.IsAuthenticated || !_currentUser.UserId.HasValue)
                throw new UnauthorizedAccessException("User not authenticated.");

            var user = await _userManager.FindByIdAsync(_currentUser.UserId.Value.ToString());
            if (user == null)
                throw new InvalidOperationException("User not found.");

            return user;
        }
        private void UpdatePerson(Person person, PersonRequestDto dto)
        {
            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            person.SecondName = dto.SecondName;
            person.ThirdName = dto.ThirdName;
            person.DateOfBirth = dto.DateOfBirth;
            person.Gender = dto.Gender;
        }

        private void UpdateAddress(Person person, AddressRequestDto dto)
        {
            if (person.Address == null)
                person.Address = new Address { Person = person };

            person.Address.Street = dto.Street;
            person.Address.City = dto.City;
            person.Address.State = dto.State;
            person.Address.LocationMapUrl = dto.LocationMapUrl;
            person.Address.Type = dto.Type;
        }

        private void UpdateContact(Person person, ContactRequestDto dto)
        {
            var contact = person.Contacts?.FirstOrDefault() ?? new Contact { PersonId = person.Id };
            contact.Type = dto.Type;
            contact.Value = dto.Value;
            contact.IsPrimary = dto.IsPrimary;

            if (person.Contacts == null)
                person.Contacts = new List<Contact>();

            if (!person.Contacts.Contains(contact))
                person.Contacts.Add(contact);
        }

        private void UpdateResidency(Person person, ResidencyRequestDto dto)
        {
            if (person.Residency == null)
                person.Residency = new Residency { PersonId = person.Id };

            person.Residency.ResidencyNumber = dto.ResidencyNumber;
            person.Residency.ValidUntil = dto.ValidUntil;
            // Optional: set CountryId if required
        }

        private void UpdateNationality(Person person, NationalityRequestDto dto)
        {
            if (person.Nationality == null)
                person.Nationality = new Nationality { PersonId = person.Id };

            person.Nationality.NationalIdNumber = dto.NationalIdNumber;
            person.Nationality.PassportNumber = dto.PassportNumber;
            // Optional: set CountryId or ResidencyId if required
        }

        #endregion

    }
}
