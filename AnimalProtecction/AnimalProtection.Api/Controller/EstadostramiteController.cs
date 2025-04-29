using System.Net;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class EstadostramiteController: ControllerBase
{
    private readonly IEstadostramiteQueryService _estadostramiteQueryService;
    private ILogger<EstadostramiteController> _logger;

    public EstadostramiteController(IEstadostramiteQueryService estadostramiteQueryService, ILogger<EstadostramiteController> logger)
    {
        _estadostramiteQueryService = estadostramiteQueryService;
        _logger = logger;
    }
    
    /// <summary>
    /// Obtiene una lista paginada de estadostramite.
    /// </summary>
    /// <param name="pageNumber">Número de página.</param>
    /// <param name="pageSize">Tamaño de página.</param>
    /// <returns>Una lista paginada de trámites.</returns>
    /// <response code="200">Devuelve la lista de estadostramite paginados.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran estadostramite.</response>
    [HttpGet("GetAllEstadostramites")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllEstadostramites([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener trámites. Página: {pageNumber}, Tamaño: {pageSize}");

        var result = await _estadostramiteQueryService.GetAllEstadostramite(pageNumber, pageSize);

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
    /// Obtiene un estadostramite por su ID.
    /// </summary>
    /// <param name="id">ID del estadostramite.</param>
    /// <returns>El estadostramite correspondiente al ID proporcionado.</returns>
    [HttpGet("GetEstadostramiteById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEstadostramiteById(Guid id)
    {
        var result = await _estadostramiteQueryService.GetEstadostramiteById<EstadostramiteRecord>(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea un nuevo estadostramite.
    /// </summary>
    /// <param name="tramiteCreateRecord">Datos del estadostramite a crear.</param>
    /// <returns>El estadostramite creado.</returns>
    [HttpPost("CreateEstadostramite")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEstadostramite([FromBody] EstadostramiteCreateRecord tramiteCreateRecord)
    {
        var result = await _estadostramiteQueryService.CreateEstadostramite(tramiteCreateRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetEstadostramiteById), new { id = result.Value.Id }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza un estadostramite existente.
    /// </summary>
    /// <param name="tramiteUpdateRecord">Datos actualizados del estadostramite.</param>
    /// <returns>El estadostramite actualizado.</returns>
    [HttpPut("UpdateEstadostramite")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEstadostramite([FromBody] EstadostramiteUpdateRecord estadostramiteUpdateRecord)
    {
        var result = await _estadostramiteQueryService.UpdateEstadostramite(estadostramiteUpdateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina un estadostramite por su ID.
    /// </summary>
    /// <param name="id">ID del estadostramite a eliminar.</param>
    /// <returns>Un resultado indicando si la eliminación fue exitosa.</returns>
    [HttpDelete("DeleteEstadostramite/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEstadostramite(Guid id)
    {
        var result = await _estadostramiteQueryService.DeleteEstadostramite(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

}