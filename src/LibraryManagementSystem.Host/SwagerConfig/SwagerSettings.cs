using Microsoft.OpenApi.Models;

namespace LibraryManagementSystem.Host.SwagerConfig
{
    public static class SwagerSettings
    {
        public static IServiceCollection AddSwagerSettings(this IServiceCollection services, IConfiguration configuration)
        {
            // -----------------------------
            // Swagger configuration
            // -----------------------------
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Library Management API",
                    Version = "v1",
                    Description = "API documentation with JWT authentication"
                });

                // JWT security definition
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                // Apply security globally
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "bearer",
                            Name = "Authorization",
                            In = ParameterLocation.Header
                        },
                        new List<string>()

                    }
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }
    }
    
}
