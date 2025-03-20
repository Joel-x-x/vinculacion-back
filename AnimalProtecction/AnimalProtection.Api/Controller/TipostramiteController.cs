using System.Net;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class TipostramiteController: ControllerBase
{
    private readonly ITipostramiteQueryService _tipostramiteQueryService;
    private ILogger<TipostramiteController> _logger;

    public TipostramiteController(ITipostramiteQueryService tipostramiteQueryService, ILogger<TipostramiteController> logger)
    {
        _tipostramiteQueryService = tipostramiteQueryService;
        _logger = logger;
    }
    
    /// <summary>
    /// Obtiene una lista paginada de tipostramite.
    /// </summary>
    /// <param name="pageNumber">Número de página.</param>
    /// <param name="pageSize">Tamaño de página.</param>
    /// <returns>Una lista paginada de trámites.</returns>
    /// <response code="200">Devuelve la lista de tipostramite paginados.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran tipostramite.</response>
    [HttpGet("GetAllTipostramites")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllTipostramites([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener trámites. Página: {pageNumber}, Tamaño: {pageSize}");

        var result = await _tipostramiteQueryService.GetAllTipostramite(pageNumber, pageSize);

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
    /// Obtiene un tipostramite por su ID.
    /// </summary>
    /// <param name="id">ID del tipostramite.</param>
    /// <returns>El tipostramite correspondiente al ID proporcionado.</returns>
    [HttpGet("GetTipostramiteById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTipostramiteById(Guid id)
    {
        var result = await _tipostramiteQueryService.GetTipostramiteById<TipostramiteRecord>(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea un nuevo tipostramite.
    /// </summary>
    /// <param name="tramiteCreateRecord">Datos del tipostramite a crear.</param>
    /// <returns>El tipostramite creado.</returns>
    [HttpPost("CreateTipostramite")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTipostramite([FromBody] TipostramiteCreateRecord tramiteCreateRecord)
    {
        var result = await _tipostramiteQueryService.CreateTipostramite(tramiteCreateRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetTipostramiteById), new { id = result.Value.Id }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza un tipostramite existente.
    /// </summary>
    /// <param name="tramiteUpdateRecord">Datos actualizados del tipostramite.</param>
    /// <returns>El tipostramite actualizado.</returns>
    [HttpPut("UpdateTipostramite")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTipostramite([FromBody] TipostramiteUpdateRecord tipostramiteUpdateRecord)
    {
        var result = await _tipostramiteQueryService.UpdateTipostramite(tipostramiteUpdateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina un tipostramite por su ID.
    /// </summary>
    /// <param name="id">ID del tipostramite a eliminar.</param>
    /// <returns>Un resultado indicando si la eliminación fue exitosa.</returns>
    [HttpDelete("DeleteTipostramite/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTipostramite(Guid id)
    {
        var result = await _tipostramiteQueryService.DeleteTipostramite(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

}