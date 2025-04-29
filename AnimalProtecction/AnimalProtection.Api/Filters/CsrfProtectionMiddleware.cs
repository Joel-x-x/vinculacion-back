using AnimalProtection.Domain.Shared;
using Microsoft.Extensions.Options;

namespace AnimalProtection.Api.Filters;

public class CsrfProtectionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly string _urlApi;
    
    public CsrfProtectionMiddleware(RequestDelegate next, IOptions<Settings> validApiKey)
    {
        _next = next;
        _urlApi = validApiKey.Value.UrlApi;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == "POST" ||
            context.Request.Method == "PUT" ||
            context.Request.Method == "DELETE")
        {
            var originHeader = context.Request.Headers["Origin"].ToString();
            var refererHeader = context.Request.Headers["Referer"].ToString();

            if (string.IsNullOrEmpty(originHeader) ||
                !originHeader.StartsWith(_urlApi, StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Invalid Origin.");
                return;
            }

            if (string.IsNullOrEmpty(refererHeader))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Referer header is missing.");
                return;
            }

            try
            {
                var refererUri = new Uri(refererHeader);
                var expectedUri = new Uri(_urlApi);

                bool sameHost = refererUri.Host.Equals(expectedUri.Host, StringComparison.OrdinalIgnoreCase);
                bool sameScheme = refererUri.Scheme.Equals(expectedUri.Scheme, StringComparison.OrdinalIgnoreCase);

                // Si no especificaste el puerto, usa el default (80 para http, 443 para https)
                int refererPort = refererUri.IsDefaultPort
                    ? (refererUri.Scheme == "https" ? 443 : 80)
                    : refererUri.Port;
                int expectedPort = expectedUri.IsDefaultPort
                    ? (expectedUri.Scheme == "https" ? 443 : 80)
                    : expectedUri.Port;

                if (!(sameHost && sameScheme && refererPort == expectedPort))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Invalid Referer.");
                    return;
                }
            }
            catch (UriFormatException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Malformed Referer URL.");
                return;
            }
        }

        await _next(context);
    }
}

