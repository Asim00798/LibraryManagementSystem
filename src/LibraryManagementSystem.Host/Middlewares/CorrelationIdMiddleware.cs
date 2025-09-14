using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Host.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderKey = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Use existing correlation ID from header or generate a new one
            if (!context.Request.Headers.TryGetValue(HeaderKey, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }

            // Add it to response headers
            context.Response.Headers[HeaderKey] = correlationId;

            // Add it to HttpContext.Items for logging
            context.Items[HeaderKey] = correlationId;

            await _next(context);
        }
    }

    public static class CorrelationIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}
