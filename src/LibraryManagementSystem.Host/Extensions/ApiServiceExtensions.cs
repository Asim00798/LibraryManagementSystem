#region Usings
using LibraryManagementSystem.Host.SwagerConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
namespace LibraryManagementSystem.Host.Extensions
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // -----------------------------
            // JWT Authentication
            // -----------------------------
            var issuer = configuration["JwtSettings:Issuer"] ?? throw new InvalidOperationException("JWT Issuer missing");
            var audience = configuration["JwtSettings:Audience"] ?? throw new InvalidOperationException("JWT Audience missing");
            var key = configuration["JwtSettings:Key"] ?? throw new InvalidOperationException("JWT Key missing");

            // -----------------------------
            // Authentication
            // -----------------------------
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // -----------------------------
            // Validate Token
            // -----------------------------
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

            // -----------------------------
            // Authorization
            // -----------------------------
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            // -----------------------------
            // Swagger configuration
            // -----------------------------
            services.AddSwagerSettings(configuration);


            return services;
        }
    }    
}
