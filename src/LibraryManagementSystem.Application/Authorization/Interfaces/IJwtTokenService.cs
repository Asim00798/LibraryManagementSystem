using LibraryManagementSystem.Application.DTOs.Security;
using LibraryManagementSystem.Domain.Entities.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Authorization.Interfaces
{
    public interface IJwtTokenService
    {
        Task<AuthResponseDto> GenerateTokenAsync(User user);
    }
}
