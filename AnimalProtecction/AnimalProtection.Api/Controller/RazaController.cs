using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class RazaController : ControllerBase
{
    private readonly IRazaQueryService _razaQueryService;
    private ILogger<RazaController> _logger;

    public RazaController(IRazaQueryService razaQueryService, ILogger<RazaController> logger)
    {
        _razaQueryService = razaQueryService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene una lista paginada de Razas.
    /// </summary>
    /// <param name="pageNumber">Número de página.</param>
    /// <param name="pageSize">Tamaño de página.</param>
    /// <returns>Una lista paginada de Razas.</returns>
    /// <response code="200">Devuelve la lista de Razas paginados.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran Razas.</response>
    [HttpGet("GetAllRazas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllRazas([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener razas. Página: {pageNumber}, Tamaño: {pageSize}");
        var result = await _razaQueryService.GetAllRazas(pageNumber, pageSize);
        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener razas: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }
        _logger.LogInformation("Razas obtenidas exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Obtiene una raza por su ID.
    /// </summary>
    /// <param name="id">ID de la raza.</param>
    /// <returns>La raza correspondiente al ID proporcionado.</returns>
    [HttpGet("GetRazaById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRazaById(Guid id)
    {
        var result = await _razaQueryService.GetRazaById<RazaRecord>(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea una nueva raza.
    /// </summary>
    /// <returns>La raza creada.</returns>
    [HttpPost("CreateRaza")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRaza([FromBody] RazaCreateRecord razaCreateRecord)
    {
        var result = await _razaQueryService.CreateRaza(razaCreateRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetRazaById), new { id = razaCreateRecord }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza un raza existente.
    /// </summary>
    /// <returns>La raza actualizado.</returns>
    [HttpPut("UpdateRaza")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRaza([FromBody] RazaUpdateRecord razaUpdateRecord)
    {
        var result = await _razaQueryService.UpdateRaza(razaUpdateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina un raza por su ID.
    /// </summary>
    /// <param name="id">ID del raza a eliminar.</param>
    /// <returns>Un resultado indicando si la eliminación fue exitosa.</returns>
    [HttpDelete("DeleteRaza/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRaza(Guid id)
    {
        var result = await _razaQueryService.DeleteRaza(id);
        return result.IsSuccess ? Ok() : NotFound(result.Error);
    }
}