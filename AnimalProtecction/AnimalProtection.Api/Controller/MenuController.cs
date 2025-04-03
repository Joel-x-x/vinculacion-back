using System.Net;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]

public class MenuController : ControllerBase
{
    private readonly IMenuQueryService _menuQueryService;
    private ILogger<MenuController> _logger;

    public MenuController(IMenuQueryService menuQueryService, ILogger<MenuController> logger)
    {
        _menuQueryService = menuQueryService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene una lista paginada de menus.
    /// </summary>
    /// <param name="pageNumber">Numero de pagina.</param>
    /// <param name="pageSize">Tamanio de pagina.</param>
    /// <returns>Una lista paginada de menus.</returns>
    /// <response code="200">Devuelve la lista de menus paginados.</response>
    /// <response code="400">Si ocurre un error al procesar la solicitud.</response>
    /// <response code="404">Si no se encuentran menus.</response>
    [HttpGet("GetAllMenus")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public  async Task<IActionResult> GetAllMenus([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation($"Inicia el proceso para obtener menus. Pagina: {pageNumber}, Tamanio: {pageSize}");

        var result = await _menuQueryService.GetAllMenus(pageNumber, pageSize);

        if(!result.IsSuccess)
        {
            _logger.LogWarning($"Error al obtener menus: {result.Error}");
            return result.Code switch
            {
                (int)HttpStatusCode.NotFound => NotFound(result.Error),
                _=> BadRequest(result.Error)
            };
        }

        _logger.LogInformation("Menus obtenidos exitosamente.");
        return Ok(result.Value);
    }

    /// <summary>
    /// Obtiene un menu por su ID.
    /// </summary>
    /// <param name="id">ID del menu.</param>
    /// <returns>El menu correspondiente al ID proporcionado.</returns>
    [HttpGet("GetMenuById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetMenuById(Guid id)
    {
        var result = await _menuQueryService.GetMenuById<MenuRecord>(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea un nuevo menu.
    /// </summary>
    /// <param name="menuCreateRecord">Datos del menu a crear.</param>
    /// <returns>El menu creado.</returns>
    [HttpPost("CreateMenu")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> CreateMenu([FromBody] MenuCreateRecord menuCreateRecord)
    {
        var result = await _menuQueryService.CreateMenu(menuCreateRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetMenuById), new { id = result.Value.Id }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza un menu existente.
    /// </summary>
    /// <param name="menuUpdateRecord">Datos actualizados del menu.</param>
    /// <returns>El menu actualizado.</returns>
    [HttpPut("UpdateMenu")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> UpdateMenu([FromBody] MenuUpdateRecord menuUpdateRecord)
    {
        var result = await _menuQueryService.UpdateMenu(menuUpdateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina un menu por su ID.
    /// </summary>
    /// <param name="id">ID del menu a eliminar.</param>
    /// <returns>Un resultado indicando si la eliminacion fue exitosa.</returns>
    [HttpDelete("DeleteMenu/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> DeleteMenu(Guid id)
    {
        var result = await _menuQueryService.DeleteMenu(id);
        return result.IsSuccess ? Ok() : NotFound(result.Error);
    }
}