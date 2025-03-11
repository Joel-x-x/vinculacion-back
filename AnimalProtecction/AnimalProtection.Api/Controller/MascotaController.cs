using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class MascotaController : ControllerBase
{
    private readonly IMascotaQueryService _mascotaQueryService;
    private ILogger<MascotaController> _logger;

    public MascotaController(IMascotaQueryService mascotaQueryService, ILogger<MascotaController> logger)
    {
        _mascotaQueryService = mascotaQueryService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene una lista paginada de Mascotas.
    /// </summary>
    /// <param name="pageNumber">Número de página.</param>
    /// <param name="pageSize">Tamaño de página.</param>
    /// <returns>Una lista paginada de Mascotas.</returns>
    /// <response code="200">Devuelve la lista de Mascotas paginados.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran Mascotas.</response>
    [HttpGet("GetAllMascotas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllMascotas([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener mascotas. Página: {pageNumber}, Tamaño: {pageSize}");
        var result = await _mascotaQueryService.GetAllMascotas(pageNumber, pageSize);
        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener mascotas: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }
        _logger.LogInformation("Mascotas obtenidas exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Obtiene una mascota por su ID.
    /// </summary>
    /// <param name="id">ID de la mascota.</param>
    /// <returns>El mascota correspondiente al ID proporcionado.</returns>
    [HttpGet("GetMascotaById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMascotaById(Guid id)
    {
        var result = await _mascotaQueryService.GetMascotaById<MascotaRecord>(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea un nuevo mascota.
    /// </summary>
    /// <returns>El mascota creado.</returns>
    [HttpPost("CreateMascota")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMascota([FromBody] MascotaCreateRecord mascotaCreateRecord)
    {
        var result = await _mascotaQueryService.CreateMascota(mascotaCreateRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetMascotaById), new { id = mascotaCreateRecord.Id }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza una mascota existente.
    /// </summary>
    /// <returns>La mascota actualizado.</returns>
    [HttpPut("UpdateMascota")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMascota([FromBody] MascotaUpdateRecord mascotaUpdateRecord)
    {
        var result = await _mascotaQueryService.UpdateMascota(mascotaUpdateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina un mascota por su ID.
    /// </summary>
    /// <param name="id">ID de la mascota a eliminar.</param>
    /// <returns>Un resultado indicando si la eliminación fue exitosa.</returns>
    [HttpDelete("DeleteMascota/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMascota(Guid id)
    {
        var result = await _mascotaQueryService.DeleteMascota(id);
        return result.IsSuccess ? Ok() : NotFound(result.Error);
    }
}