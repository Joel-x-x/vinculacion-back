using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ArchivoCampanaController : ControllerBase
{
    private readonly IArchivoCampanaQueryService _service;

    public ArchivoCampanaController(IArchivoCampanaQueryService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todos los archivos asociados a una campaña.
    /// </summary>
    /// <param name="campanaId">ID de la campaña.</param>
    /// <returns>Lista de archivos de campaña.</returns>
    [HttpGet("ByCampana/{campanaId}")]
    public async Task<IActionResult> GetArchivosByCampanaId(Guid campanaId)
    {
        var result = await _service.GetArchivosByCampanaId(campanaId);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Obtiene un archivo de campaña por su ID.
    /// </summary>
    /// <param name="id">ID del archivo de campaña.</param>
    /// <returns>Archivo de campaña.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetArchivoCampanaById(Guid id)
    {
        var result = await _service.GetArchivoCampanaById(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Crea un nuevo archivo de campaña.
    /// </summary>
    /// <param name="createRecord">Datos para crear el archivo de campaña.</param>
    /// <returns>Archivo de campaña creado.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateArchivoCampana([FromBody] ArchivoCampanaCreateRecord createRecord)
    {
        var result = await _service.CreateArchivoCampana(createRecord);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetArchivoCampanaById), new { id = result.Value.Id }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Actualiza un archivo de campaña existente.
    /// </summary>
    /// <param name="id">ID del archivo de campaña a actualizar.</param>
    /// <param name="updateRecord">Datos actualizados del archivo de campaña.</param>
    /// <returns>Archivo de campaña actualizado.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArchivoCampana(Guid id, [FromBody] ArchivoCampanaUpdateRecord updateRecord)
    {
        var result = await _service.UpdateArchivoCampana(id, updateRecord);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Elimina lógicamente un archivo de campaña.
    /// </summary>
    /// <param name="id">ID del archivo de campaña a eliminar.</param>
    /// <returns>True si la eliminación fue exitosa.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArchivoCampana(Guid id)
    {
        var result = await _service.DeleteArchivoCampana(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
} 