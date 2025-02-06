using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace AnimalProtection.Application.Commands.Token;
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Generar un JWT
    public string GenerateJwtToken(string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", userId)
            }),
            Expires = DateTime.UtcNow.AddMinutes(15), // Tiempo de expiración del JWT
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    // Generar un refresh token
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    // Validar un refresh token (simulación)
    public bool ValidateRefreshToken(string refreshToken, string userId)
    {
        // Aquí deberías verificar el refresh token en una base de datos o caché.
        // Este es un ejemplo simplificado.
        return true; // Simula que el refresh token es válido.
    }

    // Renovar un JWT utilizando un refresh token
    public string RenewToken(string expiredToken, string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        // Validar el refresh token
        var userId = GetUserIdFromToken(expiredToken);
        if (!ValidateRefreshToken(refreshToken, userId))
        {
            throw new SecurityTokenException("Refresh token inválido.");
        }

        // Generar un nuevo JWT
        return GenerateJwtToken(userId);
    }

    // Obtener el ID de usuario desde un token expirado
    private string GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        return jwtToken.Claims.First(x => x.Type == "id").Value;
    }
}