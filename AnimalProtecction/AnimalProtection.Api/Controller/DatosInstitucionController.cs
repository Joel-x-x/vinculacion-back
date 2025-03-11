using System.Net;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Application.Querys.Service;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]

public class DatosInstitucionController : ControllerBase{

    private readonly IDatosInstitucionService _datosInstitucionService;
    private ILogger<DatosInstitucionController> _logger;

    public DatosInstitucionController(IDatosInstitucionService datosInstitucionService, ILogger<DatosInstitucionController> logger)
    {
        _datosInstitucionService = datosInstitucionService;
        _logger = logger;
    }
    
    /// <summary>
    /// Obtiene una lista paginada de datos de institución.
    /// </summary>
    /// <param name="pageNumber">Número de página.</param>
    /// <param name="pageSize">Tamaño de página.</param>
    /// <returns>Una lista paginada de datos de institución.</returns>
    /// <response code="200">Devuelve la lista de datos de institución paginados.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran datos de institución.</response>
    [HttpGet("GetAllDatosInstitucion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllDatosInstitucion([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener datos de institución. Página: {pageNumber}, Tamaño: {pageSize}");

        var result = await _datosInstitucionService.GetAllDatosInstitucion(pageNumber, pageSize);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener datos de institución: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Datos de institución obtenidos exitosamente.");
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Obtiene un dato de institución por su ID.
    /// </summary>
    /// <param name="id">ID de la institución.</param>
    /// <returns>El dato de institución correspondiente al ID proporcionado.</returns>
    [HttpGet("GetDatosInstitucionById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDatosInstitucionById(Guid id)
    {
        _logger.LogInformation($"Inicia el proceso para obtener el dato de institución con ID: {id}");

        var result = await _datosInstitucionService.GetDatosInstitucionById<DatosInstitucionRecord>(id);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener el dato de institución: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Dato de institución obtenido exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Crea un nuevo dato de institución.
    /// </summary>
    /// <param name="datosInstitucionCreateRecord">Datos de institución a crear.</param>
    /// <returns>Institución creada.</returns>
    [HttpPost("CreateDatosInstitucion")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDatosInstitucion([FromBody] DatosInstitucionCreateRecord datosInstitucionCreateRecord)
    {
        _logger.LogInformation("Inicia el proceso para crear un dato de institución.");

        var result = await _datosInstitucionService.CreateDatosInstitucion(datosInstitucionCreateRecord);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al crear el dato de institución: {result.Error}");
            return BadRequest(result.Error);
        }

        _logger.LogInformation("Dato de institución creado exitosamente.");
        return CreatedAtAction(nameof(GetDatosInstitucionById), new { id = datosInstitucionCreateRecord.Id }, result.Value);
    }

    /// <summary>
    /// Actualiza un dato de institución existente.
    /// </summary>
    /// <param name="datosInstitucionUpdateRecord">Datos actualizados de la institución.</param>
    /// <returns>Institución actualizada.</returns>
    [HttpPut("UpdateDatosInstitucion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDatosInstitucion([FromBody] DatosInstitucionUpdateRecord datosInstitucionUpdateRecord)
    {
        _logger.LogInformation($"Inicia el proceso para actualizar el dato de institución con ID: {datosInstitucionUpdateRecord.Id}");

        var result = await _datosInstitucionService.UpdateDatosInstitucion(datosInstitucionUpdateRecord);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al actualizar el dato de institución: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Dato de institución actualizado exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Elimina un dato de institución existente.
    /// </summary>
    /// <param name="id">ID de la institución.</param>
    /// <returns>Indica si la institución fue eliminada correctamente.</returns>
    [HttpDelete("DeleteDatosInstitucion/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDatosInstitucion(Guid id)
    {
        _logger.LogInformation($"Inicia el proceso para eliminar el dato de institución con ID: {id}");

        var result = await _datosInstitucionService.DeleteDatosInstitucion(id);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al eliminar el dato de institución: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Dato de institución eliminado exitosamente.");
        return Ok(result.Value);
    }

}