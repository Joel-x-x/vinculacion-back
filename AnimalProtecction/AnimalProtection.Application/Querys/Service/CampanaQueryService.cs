using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using AnimalProtecction.Generated;
using AnimalProtecction.Generated.Repositories.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnimalProtection.Application.Querys.Service;

public class CampanaQueryService : ICampanaQueryService
{
    private readonly ILogger<CampanaQueryService> _logger;
    private readonly IMapper _mapper;
    private readonly AnimalprotectionContext _context;

    public CampanaQueryService(
        ILogger<CampanaQueryService> logger,
        IMapper mapper,
        AnimalprotectionContext context)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<PagedResult<CampanaRecord>>> GetAllCampanas(int pageNumber, int pageSize)
    {
        try
        {
            var query = _context.Campanas
                .Include(c => c.IdtipocampanaNavigation)
                .Where(c => c.Estaactivo == true)
                .AsNoTracking();

            var total = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CampanaRecord(
                    c.Id,
                    c.Titulo,
                    c.IdtipocampanaNavigation.Nombre,
                    c.Cuerpo,
                    c.Fechaevento,
                    c.Fechacaducidad,
                    c.Idtipocampana,
                    c.Estaactivo ?? true
                ))
                .ToListAsync();

            var result = new PagedResult<CampanaRecord>
            {
                Items = items,
                Total = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result<PagedResult<CampanaRecord>>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener las campañas");
            return Result<PagedResult<CampanaRecord>>.Failure("Error al obtener las campañas");
        }
    }

    public async Task<Result<T>> GetCampanaById<T>(Guid id) where T : class
    {
        try
        {
            var campana = await _context.Campanas
                .Include(c => c.IdtipocampanaNavigation)
                .Where(c => c.Id == id && c.Estaactivo == true)
                .Select(c => new CampanaRecord(
                    c.Id,
                    c.Titulo,
                    c.IdtipocampanaNavigation.Nombre,
                    c.Cuerpo,
                    c.Fechaevento,
                    c.Fechacaducidad,
                    c.Idtipocampana,
                    c.Estaactivo ?? true
                ))
                .FirstOrDefaultAsync();

            if (campana == null)
                return Result<T>.Failure("Campaña no encontrada");

            return Result<T>.Success(_mapper.Map<T>(campana));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la campaña");
            return Result<T>.Failure("Error al obtener la campaña");
        }
    }

    public async Task<Result<CampanaRecord>> CreateCampana(CampanaCreateRecord campanaCreateRecord)
    {
        try
        {
            var campana = new Campana
            {
                Id = Guid.NewGuid(),
                Titulo = campanaCreateRecord.Titulo,
                Cuerpo = campanaCreateRecord.Cuerpo,
                Fechaevento = campanaCreateRecord.FechaEvento,
                Fechacaducidad = campanaCreateRecord.FechaCaducidad,
                Idtipocampana = campanaCreateRecord.IdTipoCampana,
                Estaactivo = true
            };

            _context.Campanas.Add(campana);
            await _context.SaveChangesAsync();

            var createdCampana = await GetCampanaById<CampanaRecord>(campana.Id);
            return createdCampana;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear la campaña");
            return Result<CampanaRecord>.Failure("Error al crear la campaña");
        }
    }

    public async Task<Result<CampanaRecord>> UpdateCampana(Guid id, CampanaUpdateRecord campanaUpdateRecord)
    {
        try
        {
            var campana = await _context.Campanas
                .FirstOrDefaultAsync(c => c.Id == id && c.Estaactivo == true);

            if (campana == null)
                return Result<CampanaRecord>.Failure("Campaña no encontrada");

            campana.Titulo = campanaUpdateRecord.Titulo;
            campana.Cuerpo = campanaUpdateRecord.Cuerpo;
            campana.Fechaevento = campanaUpdateRecord.FechaEvento;
            campana.Fechacaducidad = campanaUpdateRecord.FechaCaducidad;
            campana.Idtipocampana = campanaUpdateRecord.IdTipoCampana;
            campana.Estaactivo = campanaUpdateRecord.EstaActivo;

            await _context.SaveChangesAsync();

            var updatedCampana = await GetCampanaById<CampanaRecord>(id);
            return updatedCampana;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar la campaña");
            return Result<CampanaRecord>.Failure("Error al actualizar la campaña");
        }
    }

    public async Task<Result<bool>> DeleteCampana(Guid id)
    {
        try
        {
            var campana = await _context.Campanas
                .FirstOrDefaultAsync(c => c.Id == id && c.Estaactivo == true);

            if (campana == null)
                return Result<bool>.Failure("Campaña no encontrada");

            campana.Estaactivo = false;
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar la campaña");
            return Result<bool>.Failure("Error al eliminar la campaña");
        }
    }
} 