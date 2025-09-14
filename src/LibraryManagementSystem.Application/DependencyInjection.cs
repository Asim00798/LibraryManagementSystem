#region Usings
using AutoMapper;
using LibraryManagement.Domain.Interfaces;
using LibraryManagementSystem.Application.Authorization.Services;
using LibraryManagementSystem.Application.Features.Interfaces;
using LibraryManagementSystem.Application.Features.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
#endregion
namespace LibraryManagementSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // -----------------------------
            // Application Services
            // -----------------------------
            // Admin Services
            services.AddScoped<IManageLanguagesService, ManageLanguagesService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRoleService, RoleService>();
            //Authorization
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

            // Library Services
            services.AddScoped<IBorrowingService, BorrowingService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            // Register AutoMapper and scan this assembly for profiles
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
