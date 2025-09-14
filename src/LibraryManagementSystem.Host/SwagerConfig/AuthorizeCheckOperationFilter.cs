using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LibraryManagementSystem.Host.SwagerConfig
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null || context.MethodInfo == null)
                return;

            var hasAuthorize = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                                 .OfType<AuthorizeAttribute>().Any() == true ||
                               context.MethodInfo.GetCustomAttributes(true)
                                 .OfType<AuthorizeAttribute>().Any();

            if (!hasAuthorize)
                return;

            operation.Security ??= new List<OpenApiSecurityRequirement>();

            operation.Security.Add(new OpenApiSecurityRequirement
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
        }
    }
}
