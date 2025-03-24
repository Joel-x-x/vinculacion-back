using System.Net;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]

public class AdopcionesController : ControllerBase
{
    private readonly IAdopcionesQueryService _adopcionesQueryService;
    private ILogger<AdopcionesController> _logger;

    public AdopcionesController(IAdopcionesQueryService adopcionesQueryService, ILogger<AdopcionesController> logger)
    {
        _adopcionesQueryService = adopcionesQueryService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene una lista paginada de adopciones.
    /// </summary>
    /// <param name="pageNumber">N�mero de p�gina.</param>
    /// <param name="pageSize">Tama�o de p�gina.</param>
    /// <returns>Una lista paginada de adopciones.</returns>
    /// <response code="200">Devuelve la lista de adopciones paginados.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran adopciones.</response>
    [HttpGet("GetAllAdopciones")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public  async Task<IActionResult> GetAllAdopciones([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener adopciones. P�gina: {pageNumber}, Tama�o: {pageSize}");

        var result = await _adopcionesQueryService.GetAllAdopciones(pageNumber, pageSize);

        if(!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener adopciones: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _=> BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Adopciones obtenidos exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Obtiene un adopcion por su ID.
    /// </summary>
    /// <param name="id">ID del adopcion.</param>
    /// <returns>El adopcion correspondiente al ID proporcionado.</returns>
    [HttpGet("GetAdopcionById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetAdopcionById(Guid id)
    {
        var result = await _adopcionesQueryService.GetAdopcionById<AdopcionesRecord>(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea un nuevo adopcion.
    /// </summary>
    /// <param name="adopcionesCreateRecord">Datos de la adopcion a crear.</param>
    /// <returns>La adopcion creada.</returns>
    [HttpPost("CreateAdopcion")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> CreateAdopcion([FromBody] AdopcionesCreateRecord adopcionesCreateRecord)
    {
        var result = await _adopcionesQueryService.CreateAdopcion(adopcionesCreateRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetAdopcionById), new { id = result.Value.Id }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza un adopcion existente.
    /// </summary>
    /// <param name="adopcionesUpdateRecord">Datos actualizados de la adopcion.</param>
    /// <returns>La adopcion actualizado.</returns>
    [HttpPut("UpdateAdopcion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> UpdateAdopcion([FromBody] AdopcionesUpdateRecord adopcionesUpdateRecord)
    {
        var result = await _adopcionesQueryService.UpdateAdopcion(adopcionesUpdateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina un adopcion por su ID.
    /// </summary>
    /// <param name="id">ID de la adopcion a eliminar.</param>
    /// <returns>Un resultado indicando si la eliminaci�n fue exitosa.</returns>
    [HttpDelete("DeleteAdopcion/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> DeleteAdopcion(Guid id)
    {
        var result = await _adopcionesQueryService.DeleteAdopcion(id);
        return result.IsSuccess ? Ok() : NotFound(result.Error);
    }

}