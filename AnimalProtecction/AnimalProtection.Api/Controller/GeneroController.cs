using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class GeneroController : ControllerBase
{
    private readonly IGeneroQueryService _generoQueryService;
    private ILogger<GeneroController> _logger;

    public GeneroController(IGeneroQueryService generoQueryService, ILogger<GeneroController> logger)
    {
        _generoQueryService = generoQueryService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene una lista paginada de trámites.
    /// </summary>
    /// <param name="pageNumber">Número de página.</param>
    /// <param name="pageSize">Tamaño de página.</param>
    /// <returns>Una lista paginada de cooperantes.</returns>
    /// <response code="200">Devuelve la lista de cooperants paginados.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran cooperantes.</response>
    [HttpGet("GetAllGeneros")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllGeneros([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener generos. Página: {pageNumber}, Tamaño: {pageSize}");
        var result = await _generoQueryService.GetAllGeneros(pageNumber, pageSize);
        if (!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener generos: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }
        _logger.LogInformation("Generos obtenidos exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Obtiene un genero por su ID.
    /// </summary>
    /// <param name="id">ID del genero.</param>
    /// <returns>El genero correspondiente al ID proporcionado.</returns>
    [HttpGet("GetGeneroById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGeneroById(Guid id)
    {
        var result = await _generoQueryService.GetGeneroById<GeneroRecord>(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea un nuevo genero.
    /// </summary>
    /// <returns>El genero creado.</returns>
    [HttpPost("CreateGenero")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateGenero([FromBody] GeneroCreateRecord generoCreateRecord)
    {
        var result = await _generoQueryService.CreateGenero(generoCreateRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetGeneroById), new { id = generoCreateRecord.Id }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza un genero existente.
    /// </summary>
    /// <returns>El genero actualizado.</returns>
    [HttpPut("UpdateGenero")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGenero([FromBody] GeneroUpdateRecord generoUpdateRecord)
    {
        var result = await _generoQueryService.UpdateGenero(generoUpdateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina un genero por su ID.
    /// </summary>
    /// <param name="id">ID del genero a eliminar.</param>
    /// <returns>Un resultado indicando si la eliminación fue exitosa.</returns>
    [HttpDelete("DeleteGenero/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGenero(Guid id)
    {
        var result = await _generoQueryService.DeleteGenero(id);
        return result.IsSuccess ? Ok() : NotFound(result.Error);
    }
}