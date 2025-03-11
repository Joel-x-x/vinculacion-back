using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class EspecieController : ControllerBase
{
    private readonly IEspecyQueryService _especieQueryService;
    private ILogger<EspecieController> _logger;

    public EspecieController(IEspecyQueryService especieQueryService, ILogger<EspecieController> logger)
    {
        _especieQueryService = especieQueryService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene una lista paginada de Especies.
    /// </summary>
    /// <param name="pageNumber">Número de página.</param>
    /// <param name="pageSize">Tamaño de página.</param>
    /// <returns>Una lista paginada de Especies.</returns>
    /// <response code="200">Devuelve la lista de Especies paginados.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran Especies.</response>
    [HttpGet("GetAllEspecies")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllEspecies([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener especies. Página: {pageNumber}, Tamaño: {pageSize}");
        var result = await _especieQueryService.GetAllEspecies(pageNumber, pageSize);
        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener especies: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }
        _logger.LogInformation("Especies obtenidas exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Obtiene un especie por su ID.
    /// </summary>
    /// <param name="id">ID del especie.</param>
    /// <returns>La especie correspondiente al ID proporcionado.</returns>
    [HttpGet("GetEspecieById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEspecieById(Guid id)
    {
        var result = await _especieQueryService.GetEspecyById<EspecyRecord>(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea un nuevo especie.
    /// </summary>
    /// <returns>El especie creado.</returns>
    [HttpPost("CreateEspecie")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEspecie([FromBody] EspecyCreateRecord especyCreateRecord)
    {
        var result = await _especieQueryService.CreateEspecy(especyCreateRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetEspecieById), new { id = especyCreateRecord.Id }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza un especie existente.
    /// </summary>
    /// <returns>El especie actualizado.</returns>
    [HttpPut("UpdateEspecie")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEspecie([FromBody] EspecyUpdateRecord especyUpdateRecord)
    {
        var result = await _especieQueryService.UpdateEspecy(especyUpdateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina un especie por su ID.
    /// </summary>
    /// <param name="id">ID del especie a eliminar.</param>
    /// <returns>Un resultado indicando si la eliminación fue exitosa.</returns>
    [HttpDelete("DeleteEspecie/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEspecie(Guid id)
    {
        var result = await _especieQueryService.DeleteEspecy(id);
        return result.IsSuccess ? Ok() : NotFound(result.Error);
    }
}