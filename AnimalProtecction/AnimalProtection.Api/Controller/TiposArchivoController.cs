using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AnimalProtection.Api.Controller
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TiposArchivoController : ControllerBase
    {
        private readonly ITiposArchivoQueryService _tiposArchivoService;
        private readonly ILogger<TiposArchivoController> _logger;

        public TiposArchivoController(ITiposArchivoQueryService tiposArchivoService, ILogger<TiposArchivoController> logger)
        {
            _tiposArchivoService = tiposArchivoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene una lista paginada de Tipos de Archivo activos.
        /// </summary>
        [HttpGet("GetAllTiposArchivo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTiposArchivo([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Obteniendo lista de Tipos de Archivo activos");
            var result = await _tiposArchivoService.GetAllTiposArchivo(pageNumber, pageSize);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Error obteniendo Tipos de Archivo: {Error}", result.Error);
                return result.Code switch
                {
                    (int)HttpStatusCode.NotFound => NotFound(result.Error),
                    _ => BadRequest(result.Error)
                };
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Obtiene un Tipo de Archivo por su ID.
        /// </summary>
        [HttpGet("GetTiposArchivoById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTiposArchivoById(Guid id)
        {
            var result = await _tiposArchivoService.GetTiposArchivoById<TiposArchivoRecord>(id);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        /// <summary>
        /// Crea un nuevo Tipo de Archivo.
        /// </summary>
        [HttpPost("CreateTiposArchivo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTiposArchivo([FromBody] TiposArchivoCreateRecord createRecord)
        {
            var result = await _tiposArchivoService.CreateTiposArchivo(createRecord);
            return result.IsSuccess
                ? CreatedAtAction(nameof(GetTiposArchivoById), new { id = createRecord.Id }, result.Value)
                : BadRequest(result.Error);
        }

        /// <summary>
        /// Actualiza un Tipo de Archivo existente.
        /// </summary>
        [HttpPut("UpdateTiposArchivo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTiposArchivo([FromBody] TiposArchivoUpdateRecord updateRecord)
        {
            var result = await _tiposArchivoService.UpdateTiposArchivo(updateRecord);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        /// <summary>
        /// Elimina lógicamente un Tipo de Archivo (marca Estaactivo = false).
        /// </summary>
        [HttpDelete("DeleteTiposArchivo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTiposArchivo(Guid id)
        {
            var result = await _tiposArchivoService.DeleteTiposArchivo(id);
            return result.IsSuccess ? Ok() : NotFound(result.Error);
        }
    }
}
