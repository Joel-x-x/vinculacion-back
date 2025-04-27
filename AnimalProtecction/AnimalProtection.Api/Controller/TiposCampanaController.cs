using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class TiposCampanaController : ControllerBase
{
    private readonly ITiposCampanaQueryService _service;

    public TiposCampanaController(ITiposCampanaQueryService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todos los tipos de campaña activos.
    /// </summary>
    /// <returns>Lista de tipos de campaña.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAll();
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Obtiene un tipo de campaña por su ID.
    /// </summary>
    /// <param name="id">ID del tipo de campaña.</param>
    /// <returns>Tipo de campaña.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetById(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea un nuevo tipo de campaña.
    /// </summary>
    /// <param name="createRecord">Datos para crear el tipo de campaña.</param>
    /// <returns>Tipo de campaña creado.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TiposCampanaCreateRecord createRecord)
    {
        var result = await _service.Create(createRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza un tipo de campaña existente.
    /// </summary>
    /// <param name="id">ID del tipo de campaña a actualizar.</param>
    /// <param name="updateRecord">Datos actualizados del tipo de campaña.</param>
    /// <returns>Tipo de campaña actualizado.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TiposCampanaUpdateRecord updateRecord)
    {
        var result = await _service.Update(id, updateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina lógicamente un tipo de campaña.
    /// </summary>
    /// <param name="id">ID del tipo de campaña a eliminar.</param>
    /// <returns>True si la eliminación fue exitosa.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.Delete(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
} 