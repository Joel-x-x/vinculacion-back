using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AnimalProtection.Application.Commands.Token;
using Microsoft.IdentityModel.Tokens;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;
    private readonly ILogger<JwtMiddleware> _logger;

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration, ITokenService tokenService, ILogger<JwtMiddleware> logger)
    {
        _next = next;
        _configuration = configuration;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            AttachUserToContext(context, token);
        }

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

            // Adjuntar el usuario al contexto
            context.Items["User"] = userId;
        }
        catch (SecurityTokenExpiredException)
        {
            _logger.LogWarning("Token expirado.");
            var refreshToken = context.Request.Headers["Refresh-Token"].FirstOrDefault();
            if (!string.IsNullOrEmpty(refreshToken))
            {
                var newToken = _tokenService.RenewToken(token, refreshToken);
                if (newToken != null)
                {
                    context.Response.Headers.Add("New-Token", newToken);
                    return;
                }
            }

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.WriteAsync("Token expirado. Por favor, renueve el token.").Wait();
            return;
        }
        catch (SecurityTokenValidationException)
        {
            _logger.LogWarning("Token inválido.");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.WriteAsync("Token inválido.").Wait();
            return;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error de autenticación.");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.WriteAsync("Error de autenticación.").Wait();
            return;
        }
    }
}