using System.Net;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Application.Querys.Service;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]

public class RolController : ControllerBase {

    private readonly IRolService _rolService;
    private ILogger<RolController> _logger;

    public RolController(IRolService rolService, ILogger<RolController> logger)
    {
        _rolService = rolService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene una lista paginada de roles.
    /// </summary>
    /// <param name="pageNumber">Número de página.</param>
    /// <param name="pageSize">Tamaño de página.</param>
    /// <returns>Una lista paginada de roles.</returns>
    /// <response code="200">Devuelve la lista de roles paginados.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran roles.</response>
    [HttpGet("GetAllRol")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetAllRol([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener roles. Página: {pageNumber}, Tamaño: {pageSize}");

        var result = await _rolService.GetAllRol(pageNumber, pageSize);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener roles: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Roles obtenidos exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Obtiene un rol por su ID.
    /// </summary>
    /// <param name="id">ID del rol.</param>
    /// <returns>Un rol.</returns>
    [HttpGet("GetRolById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetRolById([FromQuery] Guid id)
    {
        _logger.LogInformation($"Inicia el proceso para obtener un rol por ID: {id}");

        var result = await _rolService.GetRolById<RolRecord>(id);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener el rol: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Rol obtenido exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Crea un nuevo rol.
    /// </summary>
    /// <param name="rolCreateRecord">Rol a crear.</param>
    /// <returns>Rol creado.</returns>
    [HttpPost("CreateRol")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> CreateRol([FromBody] RolCreateRecord rolCreateRecord)
    {
        _logger.LogInformation("Inicia el proceso para crear un rol.");

        var result = await _rolService.CreateRol(rolCreateRecord);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al crear el rol: {result.Error}");
            return BadRequest(result.Error);
        }

        _logger.LogInformation("Rol creado exitosamente.");
        return CreatedAtAction(nameof(GetRolById), new { id = result.Value.Id }, result.Value);
    }

    /// <summary>
    /// Actualiza un rol.
    /// </summary>
    /// <param name="rolUpdateRecord">Rol a actualizar.</param>
    /// <returns>Rol actualizado.</returns>
    [HttpPut("UpdateRol")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> UpdateRol([FromBody] RolUpdateRecord rolUpdateRecord)
    {
        _logger.LogInformation($"Inicia el proceso para actualizar un rol con ID: {rolUpdateRecord.Id}");

        var result = await _rolService.UpdateRol(rolUpdateRecord);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al actualizar el rol: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Rol actualizado exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Elimina un rol.
    /// </summary>
    /// <param name="id">ID del rol.</param>
    /// <returns>Respuesta de eliminación.</returns>
    [HttpDelete("DeleteRol/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> DeleteRol([FromRoute] Guid id)
    {
        _logger.LogInformation($"Inicia el proceso para eliminar un rol con ID: {id}");

        var result = await _rolService.DeleteRol(id);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al eliminar el rol: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Rol eliminado exitosamente.");
        return Ok(result.Value);
    }

}
