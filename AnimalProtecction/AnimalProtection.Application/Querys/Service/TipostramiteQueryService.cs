using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using AnimalProtection.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service;

public class TipostramiteQueryService(ITipostramiteRepository tipostramiteRepository) : ITipostramiteQueryService
{
    public async Task<ResultResponse<PagedResponseRecord<TiposTramiteRecord>>> GetAllTipostramite(int pageNumber, int pageSize)
    {
        IQueryable<Tipostramite> query = tipostramiteRepository.GetPageableAsync()
            .Where(t => t.Estaactivo == true);

        // Obtener el total de registros antes de la paginación
        int totalRecords = await query.CountAsync();

        // Aplicar paginación
        List<Tipostramite> pagedResult = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        // Mapear Tipostramite a TipostramiteRecord
        List<TiposTramiteRecord> tipostramiteRecords = pagedResult.Select(t => new TiposTramiteRecord(t)).ToList();
        PagedResponseRecord<TiposTramiteRecord> response = new PagedResponseRecord<TiposTramiteRecord>(
            tipostramiteRecords, pageNumber, pageSize, totalRecords, 
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return ResultResponse<PagedResponseRecord<TiposTramiteRecord>>.Success(response);
    }

    public async Task<ResultResponse<TiposTramiteRecord>> GetTipostramiteById<T>(Guid id)
    {
        var tipostramite = await tipostramiteRepository.GetByIdAsync(id);
        
        if (tipostramite  == null)
        {
            return ResultResponse<TiposTramiteRecord>.Failure($"No se encontró el trámite con el id: {id}", 404);
        }

        TiposTramiteRecord tipostramiteRecord = new TiposTramiteRecord(tipostramite);
        
        return ResultResponse<TiposTramiteRecord>.Success(tipostramiteRecord);
    }

    public async Task<ResultResponse<TiposTramiteRecord>> CreateTipostramite(TiposTramiteCreateRecord tipostramiteCreateRecord)
    {
        // TODO: Validar los datos del trámite

        // Crear una nueva instancia de Tipostramite usando el método de fábrica
        var tipostramite = Tipostramite.CreateFromRecord(tipostramiteCreateRecord);

        // Guardar el trámite en la base de datos
        await tipostramiteRepository.AddAsync(tipostramite);
        await tipostramiteRepository.SaveAsync();

        // Devolver el trámite creado como respuesta
        return ResultResponse<TiposTramiteRecord>.Success(new TiposTramiteRecord(tipostramite), 201);
    }

    public async Task<ResultResponse<TiposTramiteRecord>> UpdateTipostramite(TiposTramiteUpdateRecord tipostramiteUpdateRecord)
    {
        // TODO: Validar los datos del trámite
        var tipostramite = await tipostramiteRepository.GetByIdAsync(tipostramiteUpdateRecord.Id);

        // if (Tipostramite == null)
        // {
        //     return ResultResponse<TipostramiteRecord>.Failure($"No se encontró el trámite con el id: {TipostramiteUpdateRecord.Id}", 404);
        // }
        
        tipostramite.UpdateFromRecord(tipostramiteUpdateRecord);
        
        await tipostramiteRepository.UpdateAsync(tipostramite);
        
        return ResultResponse<TiposTramiteRecord>.Success(new TiposTramiteRecord(tipostramite), 200);
    }

    public async Task<ResultResponse<bool>> DeleteTipostramite(Guid id)
    {
        var tipostramite = await tipostramiteRepository.GetByIdAsync(id);
        
        // if (tipostramite == null)
        // {
        //     return ResultResponse<bool>.Failure($"No se encontró el trámite con el id: {id}", 404);
        // }

        tipostramite.eliminar();
        
        await tipostramiteRepository.UpdateAsync(tipostramite);
        
        return ResultResponse<bool>.Success(true, 200);
    }
}