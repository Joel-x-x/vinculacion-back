using System.Net;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class CampanaController : ControllerBase
{
    private readonly ICampanaQueryService _campanaQueryService;
    private readonly ILogger<CampanaController> _logger;

    public CampanaController(ICampanaQueryService campanaQueryService, ILogger<CampanaController> logger)
    {
        _campanaQueryService = campanaQueryService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene una lista paginada de campañas activas.
    /// </summary>
    /// <param name="pageNumber">Número de página.</param>
    /// <param name="pageSize">Tamaño de página.</param>
    /// <returns>Una lista paginada de campañas.</returns>
    /// <response code="200">Devuelve la lista de campañas paginadas.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran campañas.</response>
    [HttpGet("GetAllCampanas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCampanas([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener campañas. Página: {pageNumber}, Tamaño: {pageSize}");

        var result = await _campanaQueryService.GetAllCampanas(pageNumber, pageSize);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener campañas: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Campañas obtenidas exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Obtiene una campaña por su ID.
    /// </summary>
    /// <param name="id">ID de la campaña.</param>
    /// <returns>La campaña correspondiente al ID proporcionado.</returns>
    [HttpGet("GetCampanaById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCampanaById(Guid id)
    {
        var result = await _campanaQueryService.GetCampanaById<CampanaRecord>(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea una nueva campaña.
    /// </summary>
    /// <param name="campanaCreateRecord">Datos de la campaña a crear.</param>
    /// <returns>La campaña creada.</returns>
    [HttpPost("CreateCampana")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCampana([FromBody] CampanaCreateRecord campanaCreateRecord)
    {
        var result = await _campanaQueryService.CreateCampana(campanaCreateRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetCampanaById), new { id = result.Value.Id }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza una campaña existente.
    /// </summary>
    /// <param name="id">ID de la campaña a actualizar.</param>
    /// <param name="campanaUpdateRecord">Datos actualizados de la campaña.</param>
    /// <returns>La campaña actualizada.</returns>
    [HttpPut("UpdateCampana/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCampana(Guid id, [FromBody] CampanaUpdateRecord campanaUpdateRecord)
    {
        var result = await _campanaQueryService.UpdateCampana(id, campanaUpdateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina lógicamente una campaña.
    /// </summary>
    /// <param name="id">ID de la campaña a eliminar.</param>
    /// <returns>True si la eliminación fue exitosa.</returns>
    [HttpDelete("DeleteCampana/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCampana(Guid id)
    {
        var result = await _campanaQueryService.DeleteCampana(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
} 