using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LibraryManagementSystem.Infrastructure.Persistence.Context
{
    /// <summary>
    /// Factory for design-time DbContext creation (needed for EF Core tools like migrations)
    /// </summary>
    public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();

            // Configure your connection string here
            optionsBuilder.UseSqlServer("Server=.;Database=LibraryManagementSystemDB;User Id=sa;Password=123456;TrustServerCertificate=True;");

            return new LibraryDbContext(optionsBuilder.Options, null);
        }
    }
}
