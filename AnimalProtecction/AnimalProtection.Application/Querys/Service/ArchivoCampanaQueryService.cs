using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using AnimalProtecction.Generated;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnimalProtection.Application.Querys.Service;

public class ArchivoCampanaQueryService : IArchivoCampanaQueryService
{
    private readonly ILogger<ArchivoCampanaQueryService> _logger;
    private readonly AnimalprotectionContext _context;

    public ArchivoCampanaQueryService(
        ILogger<ArchivoCampanaQueryService> logger,
        AnimalprotectionContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<ResultResponse<List<ArchivoCampanaRecord>>> GetArchivosByCampanaId(Guid campanaId)
    {
        return null;
        // try
        // {
        //     var archivos = await _context.Archivoscampanas
        //         .Include(ac => ac.IdcampanaNavigation)
        //         .Include(ac => ac.IdarchivoNavigation)
        //         .Where(ac => ac.Idcampana == campanaId && ac.Estaactivo == true)
        //         .Select(ac => new ArchivoCampanaRecord(
        //             ac.Id,
        //             ac.Idarchivo,
        //             ac.Descripcion,
        //             ac.Idcampana,
        //             ac.Estaactivo,
        //             ac.IdarchivoNavigation != null ? new ArchivoRecord(
        //                 ac.IdarchivoNavigation.Id,
        //                 ac.IdarchivoNavigation.Url,
        //                 ac.IdarchivoNavigation.Formato,
        //                 ac.IdarchivoNavigation.Idtipoarchivo,
        //                 ac.IdarchivoNavigation.Estaactivo
        //             ) : null
        //         ))
        //         .ToListAsync();
        //
        //     return ResultResponse<List<ArchivoCampanaRecord>>.Success(archivos);
        // }
        // catch (Exception ex)
        // {
        //     _logger.LogError(ex, "Error al obtener archivos de campaña");
        //     return ResultResponse<List<ArchivoCampanaRecord>>.Failure("Error al obtener archivos de campaña");
        // }
    }

    public async Task<ResultResponse<ArchivoCampanaRecord>> GetArchivoCampanaById(Guid id)
    {
        return null;
        // try
        // {
        //     var ac = await _context.Archivoscampanas
        //         .Include(a => a.IdarchivoNavigation)
        //         .FirstOrDefaultAsync(a => a.Id == id && a.Estaactivo == true);
        //
        //     if (ac == null)
        //         return ResultResponse<ArchivoCampanaRecord>.Failure("Archivo de campaña no encontrado", 404);
        //
        //     var record = new ArchivoCampanaRecord(
        //         ac.Id,
        //         ac.Idarchivo,
        //         ac.Descripcion,
        //         ac.Idcampana,
        //         ac.Estaactivo,
        //         ac.IdarchivoNavigation != null ? new ArchivoRecord(
        //             ac.IdarchivoNavigation.Id,
        //             ac.IdarchivoNavigation.Url,
        //             ac.IdarchivoNavigation.Formato,
        //             ac.IdarchivoNavigation.Idtipoarchivo,
        //             ac.IdarchivoNavigation.Estaactivo
        //         ) : null
        //     );
        //     return ResultResponse<ArchivoCampanaRecord>.Success(record);
        // }
        // catch (Exception ex)
        // {
        //     _logger.LogError(ex, "Error al obtener archivo de campaña");
        //     return ResultResponse<ArchivoCampanaRecord>.Failure("Error al obtener archivo de campaña");
        // }
    }

    public async Task<ResultResponse<ArchivoCampanaRecord>> CreateArchivoCampana(ArchivoCampanaCreateRecord createRecord)
    {
        try
        {
            var ac = new Archivoscampana
            {
                Id = Guid.NewGuid(),
                Idarchivo = createRecord.IdArchivo,
                Descripcion = createRecord.Descripcion,
                Idcampana = createRecord.IdCampana,
                Estaactivo = true
            };
            _context.Archivoscampanas.Add(ac);
            await _context.SaveChangesAsync();
            return await GetArchivoCampanaById(ac.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear archivo de campaña");
            return ResultResponse<ArchivoCampanaRecord>.Failure("Error al crear archivo de campaña");
        }
    }

    public async Task<ResultResponse<ArchivoCampanaRecord>> UpdateArchivoCampana(Guid id, ArchivoCampanaUpdateRecord updateRecord)
    {
        try
        {
            var ac = await _context.Archivoscampanas.FirstOrDefaultAsync(a => a.Id == id && a.Estaactivo == true);
            if (ac == null)
                return ResultResponse<ArchivoCampanaRecord>.Failure("Archivo de campaña no encontrado", 404);

            ac.Idarchivo = updateRecord.IdArchivo;
            ac.Descripcion = updateRecord.Descripcion;
            ac.Idcampana = updateRecord.IdCampana;
            ac.Estaactivo = updateRecord.EstaActivo;
            await _context.SaveChangesAsync();
            return await GetArchivoCampanaById(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar archivo de campaña");
            return ResultResponse<ArchivoCampanaRecord>.Failure("Error al actualizar archivo de campaña");
        }
    }

    public async Task<ResultResponse<bool>> DeleteArchivoCampana(Guid id)
    {
        try
        {
            var ac = await _context.Archivoscampanas.FirstOrDefaultAsync(a => a.Id == id && a.Estaactivo == true);
            if (ac == null)
                return ResultResponse<bool>.Failure("Archivo de campaña no encontrado", 404);
            ac.Estaactivo = false;
            await _context.SaveChangesAsync();
            return ResultResponse<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar archivo de campaña");
            return ResultResponse<bool>.Failure("Error al eliminar archivo de campaña");
        }
    }
} 