using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;

namespace AnimalProtection.Api.Filters;

public class ValidationMiddleware{
    private readonly RequestDelegate _next;
    private readonly IValidatorFactory _validatorFactory;

    public ValidationMiddleware(RequestDelegate next, IValidatorFactory validatorFactory)
    {
        _next = next;
        _validatorFactory = validatorFactory;
    } 
    public async Task InvokeAsync(HttpContext context)
    {
        // Ignorar solicitudes GET
        if (context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        var endpoint = context.GetEndpoint();
        if (endpoint == null)
        {
            await _next(context);
            return;
        }

        // Obtener el tipo esperado usando reflexión
        Type expectedType = GetExpectedTypeFromEndpoint(endpoint);
        if (expectedType == null)
        {
            Console.WriteLine("No se pudo determinar el tipo esperado para la validación.");
            await _next(context);
            return;
        }

        Console.WriteLine($"Tipo esperado detectado: {expectedType.Name}");

        object dto = null;

        // Leer el cuerpo de la solicitud
        context.Request.EnableBuffering();
        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0; // Resetear la posición del stream

        try
        {
            dto = JsonConvert.DeserializeObject(requestBody, expectedType);
        }
        catch (JsonException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync($"Invalid JSON format: {ex.Message}");
            return;
        }

        // Validar usando FluentValidation
        var validator = _validatorFactory.GetValidator(expectedType);
        if (validator != null)
        {
            var validationResult = await validator.ValidateAsync(new FluentValidation.ValidationContext<object>(dto));

            if (!validationResult.IsValid)
            {
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        message = e.ErrorMessage
                    })
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
                return;
            }
        }

        // Si todo es válido, pasar al siguiente middleware
        await _next(context);
    }

    private Type GetExpectedTypeFromEndpoint(Endpoint endpoint)
    {
        // Obtener el descriptor del controlador asociado al endpoint
        var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
        if (controllerActionDescriptor == null)
        {
            return null;
        }

        // Obtener los parámetros del método
        var parameters = controllerActionDescriptor.MethodInfo.GetParameters();
        if (parameters.Length > 0)
        {
            // Buscar el primer parámetro con el atributo [FromBody]
            var bodyParameter = parameters.FirstOrDefault(p => p.GetCustomAttribute<FromBodyAttribute>() != null);
            if (bodyParameter != null)
            {
                return bodyParameter.ParameterType;
            }
        }

        return null;
    }
}

