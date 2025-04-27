using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using AnimalProtecction.Generated;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnimalProtection.Application.Querys.Service;

public class TiposCampanaQueryService : ITiposCampanaQueryService
{
    private readonly ILogger<TiposCampanaQueryService> _logger;
    private readonly AnimalprotectionContext _context;

    public TiposCampanaQueryService(
        ILogger<TiposCampanaQueryService> logger,
        AnimalprotectionContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<ResultResponse<List<TiposCampanaRecord>>> GetAll()
    {
        try
        {
            var tipos = await _context.Tiposcampanas
                .Where(t => t.Estaactivo == true)
                .Select(t => new TiposCampanaRecord(t.Id, t.Nombre, t.Estaactivo))
                .ToListAsync();
            return ResultResponse<List<TiposCampanaRecord>>.Success(tipos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener tipos de campaña");
            return ResultResponse<List<TiposCampanaRecord>>.Failure("Error al obtener tipos de campaña");
        }
    }

    public async Task<ResultResponse<TiposCampanaRecord>> GetById(Guid id)
    {
        try
        {
            var tipo = await _context.Tiposcampanas.FirstOrDefaultAsync(t => t.Id == id && t.Estaactivo == true);
            if (tipo == null)
                return ResultResponse<TiposCampanaRecord>.Failure("Tipo de campaña no encontrado", 404);
            return ResultResponse<TiposCampanaRecord>.Success(new TiposCampanaRecord(tipo.Id, tipo.Nombre, tipo.Estaactivo));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener tipo de campaña");
            return ResultResponse<TiposCampanaRecord>.Failure("Error al obtener tipo de campaña");
        }
    }

    public async Task<ResultResponse<TiposCampanaRecord>> Create(TiposCampanaCreateRecord createRecord)
    {
        try
        {
            var tipo = new Tiposcampana
            {
                Id = Guid.NewGuid(),
                Nombre = createRecord.Nombre,
                Estaactivo = true
            };
            _context.Tiposcampanas.Add(tipo);
            await _context.SaveChangesAsync();
            return ResultResponse<TiposCampanaRecord>.Success(new TiposCampanaRecord(tipo.Id, tipo.Nombre, tipo.Estaactivo));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear tipo de campaña");
            return ResultResponse<TiposCampanaRecord>.Failure("Error al crear tipo de campaña");
        }
    }

    public async Task<ResultResponse<TiposCampanaRecord>> Update(Guid id, TiposCampanaUpdateRecord updateRecord)
    {
        try
        {
            var tipo = await _context.Tiposcampanas.FirstOrDefaultAsync(t => t.Id == id && t.Estaactivo == true);
            if (tipo == null)
                return ResultResponse<TiposCampanaRecord>.Failure("Tipo de campaña no encontrado", 404);
            tipo.Nombre = updateRecord.Nombre;
            tipo.Estaactivo = updateRecord.EstaActivo;
            await _context.SaveChangesAsync();
            return ResultResponse<TiposCampanaRecord>.Success(new TiposCampanaRecord(tipo.Id, tipo.Nombre, tipo.Estaactivo));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar tipo de campaña");
            return ResultResponse<TiposCampanaRecord>.Failure("Error al actualizar tipo de campaña");
        }
    }

    public async Task<ResultResponse<bool>> Delete(Guid id)
    {
        try
        {
            var tipo = await _context.Tiposcampanas.FirstOrDefaultAsync(t => t.Id == id && t.Estaactivo == true);
            if (tipo == null)
                return ResultResponse<bool>.Failure("Tipo de campaña no encontrado", 404);
            tipo.Estaactivo = false;
            await _context.SaveChangesAsync();
            return ResultResponse<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar tipo de campaña");
            return ResultResponse<bool>.Failure("Error al eliminar tipo de campaña");
        }
    }
} 