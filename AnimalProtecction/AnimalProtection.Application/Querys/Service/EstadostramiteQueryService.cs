using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using AnimalProtection.Repositories.Interface;
using EntityFrameworkCore.Paginate;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service;

public class EstadostramiteQueryService : IEstadostramiteQueryService
{

    private readonly IEstadostramiteRepository _estadostramiteRepository;
    
    public EstadostramiteQueryService(IEstadostramiteRepository estadostramiteRepository)
    {
        _estadostramiteRepository = estadostramiteRepository;
    }
    
    public async Task<ResultResponse<PagedResponseRecord<EstadostramiteRecord>>> GetAllEstadostramite(int pageNumber, int pageSize)
    {
        IQueryable<Estadostramite> query = _estadostramiteRepository.GetPageableAsync()
            .Where(t => t.Estaactivo == true);

        // Obtener el total de registros antes de la paginación
        int totalRecords = await query.CountAsync();

        // Aplicar paginación
        List<Estadostramite> pagedResult = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        // Mapear Estadostramite a EstadostramiteRecord
        List<EstadostramiteRecord> estadostramiteRecords = pagedResult.Select(t => new EstadostramiteRecord(t)).ToList();
        PagedResponseRecord<EstadostramiteRecord> response = new PagedResponseRecord<EstadostramiteRecord>(
            estadostramiteRecords, pageNumber, pageSize, totalRecords, 
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return ResultResponse<PagedResponseRecord<EstadostramiteRecord>>.Success(response);
    }

    public async Task<ResultResponse<EstadostramiteRecord>> GetEstadostramiteById<T>(Guid id)
    {
        var estadostramite = await _estadostramiteRepository.GetByIdAsync(id);
        
        // if (estadostramite  == null)
        // {
        //     return ResultResponse<EstadostramiteRecord>.Failure($"No se encontró el trámite con el id: {id}", 404);
        // }

        EstadostramiteRecord estadostramiteRecord = new EstadostramiteRecord(estadostramite);
        
        return ResultResponse<EstadostramiteRecord>.Success(estadostramiteRecord);
    }

    public async Task<ResultResponse<EstadostramiteRecord>> CreateEstadostramite(EstadostramiteCreateRecord estadostramiteCreateRecord)
    {
        // TODO: Validar los datos del trámite

        // Crear una nueva instancia de Estadostramite usando el método de fábrica
        var estadostramite = Estadostramite.CreateFromRecord(estadostramiteCreateRecord);

        // Guardar el trámite en la base de datos
        await _estadostramiteRepository.AddAsync(estadostramite);
        await _estadostramiteRepository.SaveAsync();

        // Devolver el trámite creado como respuesta
        return ResultResponse<EstadostramiteRecord>.Success(new EstadostramiteRecord(estadostramite), 201);
    }

    public async Task<ResultResponse<EstadostramiteRecord>> UpdateEstadostramite(EstadostramiteUpdateRecord estadostramiteUpdateRecord)
    {
        // TODO: Validar los datos del trámite
        var estadostramite = await _estadostramiteRepository.GetByIdAsync(estadostramiteUpdateRecord.Id);

        estadostramite.UpdateFromRecord(estadostramiteUpdateRecord);
        
        await _estadostramiteRepository.UpdateAsync(estadostramite);
        
        return ResultResponse<EstadostramiteRecord>.Success(new EstadostramiteRecord(estadostramite), 200);
    }

    public async Task<ResultResponse<bool>> DeleteEstadostramite(Guid id)
    {
        var estadostramite = await _estadostramiteRepository.GetByIdAsync(id);
        
        if (estadostramite == null)
        {
            return ResultResponse<bool>.Failure($"No se encontró el trámite con el id: {id}", 404);
        }

        estadostramite.eliminar();
        
        await _estadostramiteRepository.UpdateAsync(estadostramite);
        
        return ResultResponse<bool>.Success(true, 200);
    }
}