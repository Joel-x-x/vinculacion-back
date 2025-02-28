using System.Net;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class PreguntasFrecuenteController: ControllerBase
{
    private readonly IPreguntasFrecuenteQueryService _preguntasFrecuenteQueryService;
    private ILogger<PreguntasFrecuenteController> _logger;

    public PreguntasFrecuenteController(IPreguntasFrecuenteQueryService preguntasFrecuenteQueryService, ILogger<PreguntasFrecuenteController> logger)
    {
        _preguntasFrecuenteQueryService = preguntasFrecuenteQueryService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene una lista paginada de Preguntas Frecuentes.
    /// </summary>
    /// <param name="pageNumber">Número de página.</param>
    /// <param name="pageSize">Tamaño de página.</param>
    /// <returns>Una lista paginada de Preguntas Frecuentes.</returns>
    /// <response code="200">Devuelve la lista de Preguntas Frecuentes paginados.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran Preguntas Frecuentes.</response>
    [HttpGet("GetAllPreguntasFrecuente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPreguntasFrecuente([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener preguntas frecuentes. Página: {pageNumber}, Tamaño: {pageSize}");

        var result = await _preguntasFrecuenteQueryService.GetAllPreguntasFrecuente(pageNumber, pageSize);

        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener preguntas frecuentes: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Preguntas Frecuentes obtenidas exitosamente.");
        return Ok(result.Value);
    }



    /// <summary>
    /// Obtiene un  Pregunta Frecuente por su ID.
    /// </summary>
    /// <param name="id">ID del Pregunta Frecuente.</param>
    /// <returns>El  Pregunta Frecuente correspondiente al ID proporcionado.</returns>
    [HttpGet("GetPreguntaFrecuenteById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPreguntaFrecuenteById(Guid id)
    {
        var result = await _preguntasFrecuenteQueryService.GetPreguntasFrecuenteById<PreguntasFrecuenteRecord>(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea un nuevo  Pregunta Frecuente.
    /// </summary>
    /// <param name="preguntaFrecuenteCreateRecord">Datos del Pregunta Frecuente a crear.</param>
    /// <returns>El Pregunta Frecuente creado.</returns>
    [HttpPost("CreatePreguntaFrecuente")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePreguntaFrecuente([FromBody] PreguntasFrecuenteCreateRecord preguntaFrecuenteCreateRecord)
    {
        var result = await _preguntasFrecuenteQueryService.CreatePreguntasFrecuente(preguntaFrecuenteCreateRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetPreguntaFrecuenteById), new { id = preguntaFrecuenteCreateRecord.Id }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza un Pregunta Frecuente existente.
    /// </summary>
    /// <param name="preguntaFrecuenteUpdateRecord">Datos actualizados del Pregunta Frecuente.</param>
    /// <returns>El Pregunta Frecuente actualizado.</returns>
    [HttpPut("UpdatePreguntaFrecuente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePreguntaFrecuente([FromBody] PreguntasFrecuenteUpdateRecord preguntaFrecuenteUpdateRecord)
    {
        var result = await _preguntasFrecuenteQueryService.UpdatePreguntasFrecuente(preguntaFrecuenteUpdateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina un Pregunta Frecuente por su ID.
    /// </summary>
    /// <param name="id">ID del Pregunta Frecuente a eliminar.</param>
    /// <returns>Un resultado indicando si la eliminación fue exitosa.</returns>
    [HttpDelete("DeletePreguntaFrecuente/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePreguntaFrecuente(Guid id)
    {
        var result = await _preguntasFrecuenteQueryService.DeletePreguntasFrecuente(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
}