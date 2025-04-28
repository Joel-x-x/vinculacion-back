using AnimalProtection.Application.Commands.Token;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{

    // Commit de prueba2222
    // Commit de prueba 33333
    private readonly ITokenService _tokenService;
    private readonly IUsuarioQueryService _usuarioQueryService;

    public AuthController(
        ITokenService tokenService,
        IUsuarioQueryService usuarioQueryService)
    {
        _tokenService = tokenService;
        _usuarioQueryService = usuarioQueryService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto request)
    {
        // Simulación de autenticación
        var user = await _usuarioQueryService.Login(request);

        if (!user.IsSuccess)
        {
            return BadRequest("Credenciales inválidas");
        }

        // Generar JWT y refresh token
        var jwtToken = _tokenService.GenerateJwtToken((user.Value.Id).ToString());
        var refreshToken = _tokenService.GenerateRefreshToken();

        return Ok(new { Token = jwtToken, RefreshToken = refreshToken });
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
    {
        var result = await _usuarioQueryService.RegisterUser(request);
        
        // Simulación de autenticación
        // var userId = "123"; // Obtén el ID del usuario después de validar las credenciales.
        // Generar JWT y refresh token
        // var jwtToken = _tokenService.GenerateJwtToken(userId);
        // var refreshToken = _tokenService.GenerateRefreshToken();

        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public IActionResult RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        var newToken = _tokenService.RenewToken(request.ExpiredToken, request.RefreshToken);
        return Ok(new { Token = newToken });
    }
}