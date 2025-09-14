using LibraryManagement.Domain.Interfaces;
using LibraryManagementSystem.Application.Authorization.Interfaces;
using LibraryManagementSystem.Application.DTOs.Request;
using LibraryManagementSystem.Application.DTOs.Security;
using LibraryManagementSystem.Application.Features.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace LibraryManagementSystem.Application.Features.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticationService(
            IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            IJwtTokenService jwtTokenService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task RegisterAsync(FullRegistrationDto request)
        {
            if (request.Person == null || request.User == null)
                throw new ArgumentException("Person, User, and Registration details are required.");

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // 1️⃣ Create Person
                var person = await CreatePersonAsync(request.Person);

                // 2️⃣ Create optional entities
                if (request.Address != null)
                    await CreateAddressAsync(person, request.Address);

                if (request.ContactRequestDto != null)
                    await CreateContactAsync(person, request.ContactRequestDto);

                if (request.Residency != null)
                    await CreateResidencyAsync(person, request.Residency);

                if (request.Nationality != null)
                    await CreateNationalityAsync(person, request.Nationality);

                // 3️⃣ Create Registration
                var registration = await CreateRegistrationAsync(person);

                // 4️⃣ Create User
                var user = await CreateUserAsync(registration, request.User);

                // Link User to Registration
                registration.User = user;
                await _unitOfWork.Registrations.UpdateAsync(registration);

                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Identifier))
                throw new ArgumentException("Username or email must be provided.");

            // Use UserManager for querying Identity
            var user = await _userManager.FindByNameAsync(request.Identifier)
                       ?? await _userManager.FindByEmailAsync(request.Identifier);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new UnauthorizedAccessException("Invalid username/email or password.");

            return await _jwtTokenService.GenerateTokenAsync(user);
        }

        #region Private Helpers

        private async Task<Person> CreatePersonAsync(PersonRequestDto dto)
        {
            var person = new Person
            {
                FirstName = dto.FirstName,
                SecondName = dto.SecondName,
                ThirdName = dto.ThirdName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                ProfileImageUrl = dto.ProfileImageUrl
            };

            await _unitOfWork.People.AddAsync(person);
            await _unitOfWork.CompleteAsync();
            return person;
        }

        private async Task<Address> CreateAddressAsync(Person person, AddressRequestDto dto)
        {
            var address = new Address
            {
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                LocationMapUrl = dto.LocationMapUrl,
                Type = dto.Type
            };

            await _unitOfWork.Addresses.AddAsync(address);
            await _unitOfWork.CompleteAsync();

            person.AddressId = address.Id;
            await _unitOfWork.People.UpdateAsync(person);
            await _unitOfWork.CompleteAsync();

            return address;
        }

        private async Task<Contact> CreateContactAsync(Person person, ContactRequestDto dto)
        {
            var contact = new Contact
            {
                PersonId = person.Id,
                Type = dto.Type,
                Value = dto.Value,
                IsPrimary = dto.IsPrimary
            };

            await _unitOfWork.Contacts.AddAsync(contact);
            await _unitOfWork.CompleteAsync();
            return contact;
        }

        private async Task<Residency> CreateResidencyAsync(Person person, ResidencyRequestDto dto)
        {
            var residency = new Residency
            {
                PersonId = person.Id,
                ResidencyNumber = dto.ResidencyNumber,
                ValidUntil = dto.ValidUntil,
                CountryId = Guid.NewGuid() // Map properly if needed
            };

            await _unitOfWork.Residencies.AddAsync(residency);
            await _unitOfWork.CompleteAsync();
            return residency;
        }

        private async Task<Nationality> CreateNationalityAsync(Person person, NationalityRequestDto dto)
        {
            var nationality = new Nationality
            {
                PersonId = person.Id,
                NationalIdNumber = dto.NationalIdNumber,
                PassportNumber = dto.PassportNumber,
                CountryId = Guid.NewGuid() // Map properly if needed
            };

            await _unitOfWork.Nationalities.AddAsync(nationality);
            await _unitOfWork.CompleteAsync();
            return nationality;
        }

        private async Task<Registration> CreateRegistrationAsync(Person person)
        {
            var registration = new Registration
            {
                PersonId = person.Id,
                Status = RegistrationStatus.Completed
            };

            await _unitOfWork.Registrations.AddAsync(registration);
            await _unitOfWork.CompleteAsync();
            return registration;
        }

        private async Task<User> CreateUserAsync(Registration registration, UserRequestDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                RegistrationId = registration.Id
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, "User");
            return user;
        }

        #endregion
    }
}
