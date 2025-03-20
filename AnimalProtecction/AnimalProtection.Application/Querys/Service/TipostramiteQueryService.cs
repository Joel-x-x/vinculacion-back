using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using EntityFrameworkCore.Paginate;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service;

public class TipostramiteQueryService : ITipostramiteQueryService
{

    private readonly ITipostramiteRepository _tipostramiteRepository;
    
    public TipostramiteQueryService(ITipostramiteRepository tipostramiteRepository)
    {
        _tipostramiteRepository = tipostramiteRepository;
    }
    
    public async Task<ResultResponse<PagedResponseRecord<TipostramiteRecord>>> GetAllTipostramite(int pageNumber, int pageSize)
    {
        IQueryable<Tipostramite> query = _tipostramiteRepository.GetPageableAsync()
            .Where(t => t.Estaactivo == true);

        // Obtener el total de registros antes de la paginación
        int totalRecords = await query.CountAsync();

        // Aplicar paginación
        List<Tipostramite> pagedResult = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        // Mapear Tipostramite a TipostramiteRecord
        List<TipostramiteRecord> tipostramiteRecords = pagedResult.Select(t => new TipostramiteRecord(t)).ToList();
        PagedResponseRecord<TipostramiteRecord> response = new PagedResponseRecord<TipostramiteRecord>(
            tipostramiteRecords, pageNumber, pageSize, totalRecords, 
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return ResultResponse<PagedResponseRecord<TipostramiteRecord>>.Success(response);
    }

    public async Task<ResultResponse<TipostramiteRecord>> GetTipostramiteById<T>(Guid id)
    {
        var tipostramite = await _tipostramiteRepository.GetByIdAsync(id);
        
        if (tipostramite  == null)
        {
            return ResultResponse<TipostramiteRecord>.Failure($"No se encontró el trámite con el id: {id}", 404);
        }

        TipostramiteRecord tipostramiteRecord = new TipostramiteRecord(Tipostramite);
        
        return ResultResponse<TipostramiteRecord>.Success(tipostramiteRecord);
    }

    public async Task<ResultResponse<TipostramiteRecord>> CreateTipostramite(TipostramiteCreateRecord tipostramiteCreateRecord)
    {
        // TODO: Validar los datos del trámite

        // Crear una nueva instancia de Tipostramite usando el método de fábrica
        var tipostramite = Tipostramite.CreateFromRecord(tipostramiteCreateRecord);

        // Guardar el trámite en la base de datos
        await _tipostramiteRepository.AddAsync(tipostramite);
        await _tipostramiteRepository.SaveAsync();

        // Devolver el trámite creado como respuesta
        return ResultResponse<TipostramiteRecord>.Success(new TipostramiteRecord(tipostramite), 201);
    }

    public async Task<ResultResponse<TipostramiteRecord>> UpdateTipostramite(TipostramiteUpdateRecord tipostramiteUpdateRecord)
    {
        // TODO: Validar los datos del trámite
        var tipostramite = await _tipostramiteRepository.GetByIdAsync(tipostramiteUpdateRecord.Id);

        // if (Tipostramite == null)
        // {
        //     return ResultResponse<TipostramiteRecord>.Failure($"No se encontró el trámite con el id: {TipostramiteUpdateRecord.Id}", 404);
        // }
        
        tipostramite.UpdateFromRecord(tipostramiteUpdateRecord);
        
        await _tipostramiteRepository.UpdateAsync(tipostramite);
        
        return ResultResponse<TipostramiteRecord>.Success(new TipostramiteRecord(tipostramite), 200);
    }

    public async Task<ResultResponse<bool>> DeleteTipostramite(Guid id)
    {
        var tipostramite = await _tipostramiteRepository.GetByIdAsync(id);
        
        if (tipostramite == null)
        {
            return ResultResponse<bool>.Failure($"No se encontró el trámite con el id: {id}", 404);
        }

        tipostramite.eliminar();
        
        await _tipostramiteRepository.UpdateAsync(tipostramite);
        
        return ResultResponse<bool>.Success(true, 200);
    }
}