using AnimalProtection.Domain.Shared;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AnimalProtection.Api.Filters
{
    public class RecaptchaMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _recaptchaSecretKey;
        private readonly HttpClient _httpClient;

        public RecaptchaMiddleware(RequestDelegate next, IOptions<Settings> configuration, HttpClient httpClient)
        {
            _next = next;
            _recaptchaSecretKey = configuration.Value.ApiKey; 
            _httpClient = httpClient;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Verifica si la cabecera Recaptcha-Token está presente en la solicitud
            if (context.Request.Headers.ContainsKey("Recaptcha-Token"))
            {
                var recaptchaToken = context.Request.Headers["Recaptcha-Token"].ToString();

                // Validar el token con Google reCAPTCHA
                var isValid = await ValidateRecaptchaTokenAsync(recaptchaToken);
                if (!isValid)
                {
                    // Si la validación falla, devuelve una respuesta 400 Bad Request
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("reCAPTCHA validation failed.");
                    return;
                }
            }

            // Si la cabecera no está presente o la validación fue exitosa, sigue con la siguiente solicitud
            await _next(context);
        }

        private async Task<bool> ValidateRecaptchaTokenAsync(string recaptchaToken)
        {
            var secretKey = _recaptchaSecretKey;

            // Construir la URL de validación del reCAPTCHA
            var validationUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={recaptchaToken}";

            try
            {
                // Realizar la solicitud POST a la API de Google para verificar el token
                var response = await _httpClient.PostAsync(validationUrl, null);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Parsear la respuesta JSON
                    var result = JObject.Parse(jsonResponse);

                    // Verificar si el campo "success" es verdadero
                    return result.Value<bool>("success");
                }
            }
            catch (Exception ex)
            {
                // Loguear el error si algo falla al realizar la solicitud
                // Aquí puedes agregar un servicio de logging si lo necesitas
                Console.WriteLine($"Error validating reCAPTCHA: {ex.Message}");
            }

            return false;
        }
    }
}
