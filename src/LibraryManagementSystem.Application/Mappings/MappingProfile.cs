using AutoMapper;
using LibraryManagementSystem.Application.DTOs.Request;
using LibraryManagementSystem.Application.DTOs.Security;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Entities.Security;

namespace LibraryManagementSystem.Application.Mappings
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            // -------------------------------
            // Person <-> PersonRequestDto
            // -------------------------------
            CreateMap<PersonRequestDto, Person>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AddressId, opt => opt.Ignore())
                .ForMember(dest => dest.Contacts, opt => opt.Ignore())
                .ForMember(dest => dest.Nationality, opt => opt.Ignore());

            CreateMap<Person, PersonRequestDto>();

            // -------------------------------
            // Address <-> AddressRequestDto
            // -------------------------------
            CreateMap<AddressRequestDto, Address>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Person, opt => opt.Ignore());

            CreateMap<Address, AddressRequestDto>();

            // -------------------------------
            // Contact <-> ContactRequestDto
            // -------------------------------
            CreateMap<ContactRequestDto, Contact>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Person, opt => opt.Ignore())
                .ForMember(dest => dest.PersonId, opt => opt.Ignore());

            CreateMap<Contact, ContactRequestDto>();

            // -------------------------------
            // Residency <-> ResidencyRequestDto
            // -------------------------------
            CreateMap<ResidencyRequestDto, Residency>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Person, opt => opt.Ignore())
                .ForMember(dest => dest.PersonId, opt => opt.Ignore())
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.CountryId, opt => opt.Ignore());

            CreateMap<Residency, ResidencyRequestDto>();

            // -------------------------------
            // Nationality <-> NationalityRequestDto
            // -------------------------------
            CreateMap<NationalityRequestDto, Nationality>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PersonId, opt => opt.Ignore())
                .ForMember(dest => dest.Person, opt => opt.Ignore())
                .ForMember(dest => dest.CountryId, opt => opt.Ignore())
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.ResidencyId, opt => opt.Ignore());
                

            CreateMap<Nationality, NationalityRequestDto>();

            // -------------------------------
            // Registration <-> RegistrationRequestDto
            // -------------------------------
            CreateMap<RegistrationRequestDto, Registration>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PersonId, opt => opt.Ignore())
                .ForMember(dest => dest.Person, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Registration, RegistrationRequestDto>();

            // -------------------------------
            // User <-> UserRequestDto
            // -------------------------------
            CreateMap<UserRequestDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrationId, opt => opt.Ignore())
                .ForMember(dest => dest.Registration, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<User, UserRequestDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore()); // Do not map password hash back
        }
    }
}
