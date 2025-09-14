#region Usings
using LibraryManagement.Domain.Interfaces;
using LibraryManagementSystem.Infrastructure.Persistence.Context;
using LibraryManagementSystem.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#endregion
namespace LibraryManagementSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // -----------------------------
            // Configure DbContext
            // -----------------------------
            services.AddDbContext<LibraryDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // -----------------------------
            // Register Unit of Work
            // -----------------------------
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

            return services;
        }
    }
}
