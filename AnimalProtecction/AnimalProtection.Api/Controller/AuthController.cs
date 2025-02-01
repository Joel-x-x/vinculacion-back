using AnimalProtection.Application.Commands.Token;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{

    // Commit de prueba 33333
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Simulación de autenticación
        var userId = "123"; // Obtén el ID del usuario después de validar las credenciales.

        // Generar JWT y refresh token
        var jwtToken = _tokenService.GenerateJwtToken(userId);
        var refreshToken = _tokenService.GenerateRefreshToken();

        return Ok(new { Token = jwtToken, RefreshToken = refreshToken });
    }

    [HttpPost("refresh-token")]
    public IActionResult RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        var newToken = _tokenService.RenewToken(request.ExpiredToken, request.RefreshToken);
        return Ok(new { Token = newToken });
    }
}