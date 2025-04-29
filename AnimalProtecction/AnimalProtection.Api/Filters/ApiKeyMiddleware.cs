using System.Net;
using AnimalProtection.Domain.Shared;
using Microsoft.Extensions.Options;

namespace ClinicMedicalAppointments.Api.Filters;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _validApiKey;

    public ApiKeyMiddleware(RequestDelegate next, IOptions<Settings> validApiKey)
    {
        _next = next;
        _validApiKey = validApiKey.Value.ApiKey;
    }

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Verificar que la cabecera x-api-key exista
        if (!context.Request.Headers.ContainsKey("x-api-key"))
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound; 
            await context.Response.WriteAsync("API Key is missing.");
            return;
        }

        // Obtener el valor del keyApi desde la cabecera
        var apiKey = context.Request.Headers["x-api-key"].ToString();

        // Validar que el keyApi sea correcto
        if (apiKey != _validApiKey)
        {
            context.Response.StatusCode = 403; // Forbidden
            await context.Response.WriteAsync("Invalid API Key.");
            return;
        }

        // Si es válido, continúa con la ejecución del pipeline
        await _next(context);
    }
}