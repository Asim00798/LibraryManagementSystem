#region Usings
using LibraryManagementSystem.Application;
using LibraryManagementSystem.Application.Authorization.Configuration;
using LibraryManagementSystem.Application.Authorization.Interfaces;
using LibraryManagementSystem.Application.Authorization.Services;
using LibraryManagementSystem.Domain.Entities.Security;
using LibraryManagementSystem.Domain.Interfaces.Security;
using LibraryManagementSystem.Infrastructure;
using LibraryManagementSystem.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#endregion
namespace LibraryManagementSystem.Host
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHostServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // ----------------------------------------------------
            // Infrastructure (DbContext, Repositories, UnitOfWork)
            // ----------------------------------------------------
            services.AddInfrastructure(configuration);

            // --------------------------------
            // Identity setup (after DbContext)
            // --------------------------------
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<LibraryDbContext>()
            .AddDefaultTokenProviders();

            // -------------------------------------------------------------
            // Application services (Business Logic, Authorization handlers)
            // -------------------------------------------------------------
            services.AddApplication(configuration);

            // ------------------
            // JWT Token service
            // ------------------
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            // --------------------------------
            // Current User (HttpContext based)
            // --------------------------------
            services.AddScoped<ICurrentUser, CurrentUser>();

            return services;
        }
    }
}
