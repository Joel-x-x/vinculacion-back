using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using EntityFrameworkCore.Paginate;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service;

public class TramiteQueryService : ITramiteQueryService
{

    private readonly ITramiteRepository _tramiteRepository;
    
    public TramiteQueryService(ITramiteRepository tramiteRepository)
    {
        _tramiteRepository = tramiteRepository;
    }
    
    public async Task<ResultResponse<PagedResponseRecord<TramiteRecord>>> GetAllTramite(int pageNumber, int pageSize)
    {
        IQueryable<Tramite> query = _tramiteRepository.GetPageableAsync()
            .Include(t => t.IdtipotramiteNavigation)
            .Include(t => t.IdestadotramiteNavigation)
            .Include(t => t.Archivostramites)
            .Where(t => t.Estaactivo == true);

        // Obtener el total de registros antes de la paginación
        int totalRecords = await query.CountAsync();

        // Aplicar paginación
        List<Tramite> pagedResult = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        // Mapear Tramite a TramiteRecord
        List<TramiteRecord> tramiteRecords = pagedResult.Select(t => new TramiteRecord(t)).ToList();
        PagedResponseRecord<TramiteRecord> response = new PagedResponseRecord<TramiteRecord>(
            tramiteRecords, pageNumber, pageSize, totalRecords, 
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return ResultResponse<PagedResponseRecord<TramiteRecord>>.Success(response);
    }

    public async Task<ResultResponse<TramiteRecord>> GetTramiteById<T>(Guid id)
    {
        var tramite = await _tramiteRepository.GetByIdAsync(id);
        
        if (tramite  == null)
        {
            return ResultResponse<TramiteRecord>.Failure($"No se encontró el trámite con el id: {id}", 404);
        }

        TramiteRecord tramiteRecord = new TramiteRecord(tramite);
        
        return ResultResponse<TramiteRecord>.Success(tramiteRecord);
    }

    public async Task<ResultResponse<TramiteRecord>> CreateTramite(TramiteCreateRecord tramiteCreateRecord)
    {
        // TODO: Validar los datos del trámite

        // Crear una nueva instancia de Tramite usando el método de fábrica
        var tramite = Tramite.CreateFromRecord(tramiteCreateRecord);

        // Guardar el trámite en la base de datos
        await _tramiteRepository.AddAsync(tramite);
        await _tramiteRepository.SaveAsync();

        // Devolver el trámite creado como respuesta
        return ResultResponse<TramiteRecord>.Success(new TramiteRecord(tramite), 201);
    }

    public async Task<ResultResponse<TramiteRecord>> UpdateTramite(TramiteUpdateRecord tramiteUpdateRecord)
    {
        // TODO: Validar los datos del trámite
        var tramite = await _tramiteRepository.GetByIdAsync(tramiteUpdateRecord.Id);

        // if (tramite == null)
        // {
        //     return ResultResponse<TramiteRecord>.Failure($"No se encontró el trámite con el id: {tramiteUpdateRecord.Id}", 404);
        // }
        
        tramite.UpdateFromRecord(tramiteUpdateRecord);
        
        await _tramiteRepository.UpdateAsync(tramite);
        
        return ResultResponse<TramiteRecord>.Success(new TramiteRecord(tramite), 200);
    }

    public async Task<ResultResponse<bool>> DeleteTramite(Guid id)
    {
        var tramite = await _tramiteRepository.GetByIdAsync(id);
        
        if (tramite == null)
        {
            return ResultResponse<bool>.Failure($"No se encontró el trámite con el id: {id}", 404);
        }

        tramite.eliminar();
        
        await _tramiteRepository.UpdateAsync(tramite);
        
        return ResultResponse<bool>.Success(true, 200);
    }
}