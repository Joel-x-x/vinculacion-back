using System.Net;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class TramiteController: ControllerBase
{
    private readonly ITramiteQueryService _tramiteQueryService;
    private ILogger<TramiteController> _logger;

    public TramiteController(ITramiteQueryService tramiteQueryService, ILogger<TramiteController> logger)
    {
        _tramiteQueryService = tramiteQueryService;
        _logger = logger;
    }
    
    /// <summary>
    /// Obtiene una lista paginada de trámites.
    /// </summary>
    /// <param name="pageNumber">Número de página.</param>
    /// <param name="pageSize">Tamaño de página.</param>
    /// <returns>Una lista paginada de trámites.</returns>
    /// <response code="200">Devuelve la lista de trámites paginados.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran trámites.</response>
    [HttpGet("GetAllTramites")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllTramites([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener trámites. Página: {pageNumber}, Tamaño: {pageSize}");

        var result = await _tramiteQueryService.GetAllTramite(pageNumber, pageSize);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener trámites: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Trámites obtenidos exitosamente.");
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Obtiene un trámite por su ID.
    /// </summary>
    /// <param name="id">ID del trámite.</param>
    /// <returns>El trámite correspondiente al ID proporcionado.</returns>
    [HttpGet("GetTramiteById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTramiteById(Guid id)
    {
        var result = await _tramiteQueryService.GetTramiteById<TramiteRecord>(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea un nuevo trámite.
    /// </summary>
    /// <param name="tramiteCreateRecord">Datos del trámite a crear.</param>
    /// <returns>El trámite creado.</returns>
    [HttpPost("CreateTramite")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTramite([FromBody] TramiteCreateRecord tramiteCreateRecord)
    {
        var result = await _tramiteQueryService.CreateTramite(tramiteCreateRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetTramiteById), new { id = result.Value.Id }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza un trámite existente.
    /// </summary>
    /// <param name="tramiteUpdateRecord">Datos actualizados del trámite.</param>
    /// <returns>El trámite actualizado.</returns>
    [HttpPut("UpdateTramite")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTramite([FromBody] TramiteUpdateRecord tramiteUpdateRecord)
    {
        var result = await _tramiteQueryService.UpdateTramite(tramiteUpdateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina un trámite por su ID.
    /// </summary>
    /// <param name="id">ID del trámite a eliminar.</param>
    /// <returns>Un resultado indicando si la eliminación fue exitosa.</returns>
    [HttpDelete("DeleteTramite/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTramite(Guid id)
    {
        var result = await _tramiteQueryService.DeleteTramite(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

}