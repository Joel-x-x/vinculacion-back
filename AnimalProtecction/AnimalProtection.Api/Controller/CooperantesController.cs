using System.Net;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]

public class CooperantesController : ControllerBase
{
    private readonly ICooperantesQueryService _cooperantesQueryService;
    private ILogger<CooperantesController> _logger;

    public CooperantesController(ICooperantesQueryService cooperantesQueryService, ILogger<CooperantesController> logger)
    {
        _cooperantesQueryService = cooperantesQueryService;
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
    [HttpGet("GetAllCooperantes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public  async Task<IActionResult> GetAllCooperantes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener cooperantes. Página: {pageNumber}, Tamaño: {pageSize}");

        var result = await _cooperantesQueryService.GetAllCooperantes(pageNumber, pageSize);

        if(!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener cooperantes: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _=> BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Cooperantes obtenidos exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Obtiene un cooperante por su ID.
    /// </summary>
    /// <param name="id">ID del cooperante.</param>
    /// <returns>El cooperante correspondiente al ID proporcionado.</returns>
    [HttpGet("GetCooperanteById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetCooperanteById(Guid id)
    {
        var result = await _cooperantesQueryService.GetCooperanteById<CooperantesRecord>(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea un nuevo cooperante.
    /// </summary>
    /// <param name="cooperantesCreateRecord">Datos del cooperante a crear.</param>
    /// <returns>El cooperante creado.</returns>
    [HttpPost("CreateCooperante")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> CreateCooperante([FromBody] CooperantesCreateRecord cooperantesCreateRecord)
    {
        var result = await _cooperantesQueryService.CreateCooperante(cooperantesCreateRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetCooperanteById), new { id = cooperantesCreateRecord.Id}, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza un cooperante existente.
    /// </summary>
    /// <param name="cooperantesUpdateRecord">Datos actualizados del cooperante.</param>
    /// <returns>El cooperante actualizado.</returns>
    [HttpPut("UpdateCooperante")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> UpdateCooperante([FromBody] CooperantesUpdateRecord cooperantesUpdateRecord)
    {
        var result = await _cooperantesQueryService.UpdateCooperante(cooperantesUpdateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina un cooperante por su ID.
    /// </summary>
    /// <param name="id">ID del cooperante a eliminar.</param>
    /// <returns>Un resultado indicando si la eliminación fue exitosa.</returns>
    [HttpDelete("DeleteCooperante/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> DeleteCooperante(Guid id)
    {
        var result = await _cooperantesQueryService.DeleteCooperante(id);
        return result.IsSuccess ? Ok() : NotFound(result.Error);
    }

}