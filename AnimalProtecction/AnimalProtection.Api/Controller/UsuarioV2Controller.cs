using System.Net;
using AnimalProtection.Application.Querys.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("2.0")]
[ApiController]
[Route("api/v{version:apiVersion}/usuarios")]

public class UsuarioV2Controller : ControllerBase
{
    private readonly IUsuarioQueryService _usuarioQueryService;
    private ILogger<UsuarioController> _logger;

    public UsuarioV2Controller(IUsuarioQueryService usuarioQueryService, ILogger<UsuarioController> logger)
    {
        _usuarioQueryService = usuarioQueryService;
        _logger = logger;
    }
    
    /// <summary>
    /// Obtiene todos los usuarios registrados.
    /// </summary>
    /// <returns>Una lista de usuarios.</returns>
    /// <response code="200">Devuelve la lista de usuarios.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran usuarios.</response>
    [HttpGet("GetAllV2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _usuarioQueryService.GetAllUser();
        var enhancedResult = result.Value.Select(user => new
        {
            user.Id,
            user.Nombre,
            user.Email,
            IsVerified = true
        });
        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener el usuario: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Usuario obtenido exitosamente.");
        return Ok(result.Value);
    }
}