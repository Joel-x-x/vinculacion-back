using System.Net;
using Microsoft.AspNetCore.Mvc;
using AnimalProtection.Application.Querys.Interface;
namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/usuarios")]
//[ApiExplorerSettings(GroupName = "UsuarioV1")]
public class UsuarioController : ControllerBase
{ 
    private readonly IUsuarioQueryService _usuarioQueryService;
    private ILogger<UsuarioController> _logger;

    public UsuarioController(IUsuarioQueryService usuarioQueryService, ILogger<UsuarioController> logger)
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
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Inicia el proceso para obtener todos los usuarios.");

            var result = await _usuarioQueryService.GetAllUser();

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
    
    /// <summary>
    /// Obtiene un usuario por su dirección de correo electrónico.
    /// </summary>
    /// <param name="email">La dirección de correo electrónico del usuario.</param>
    /// <returns>El usuario correspondiente al correo electrónico.</returns>
    /// <response code="200">Devuelve el usuario encontrado.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentra el usuario.</response>
    [HttpGet("GetUserByEmail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserByEmail([FromQueryAttribute] string email)
    {
        _logger.LogInformation("Inicia el proceso para obtener todos los usuarios.");
        
        var result = await _usuarioQueryService.GetUserByEmail(email);

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