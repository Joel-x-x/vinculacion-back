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
            .Include(t => t.Archivostramites);

        // Obtener el total de registros antes de la paginación
        int totalRecords = await query.CountAsync();

        // Aplicar paginación
        List<Tramite> pagedResult = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        // Mapear Tramite a TramiteRecord
        List<TramiteRecord> tramiteRecords = pagedResult.Select(t => new TramiteRecord(
            t.Id, t.Nombre, t.Apellido, t.Email, t.Contacto ?? "",
            t.Datos ?? "", t.Fecha, t.Numerotramite, t.Direccion ?? "",
            t.Idtipotramite, t.Idestadotramite, t.Estaactivo ?? false,
            new TipoTramiteRecord(t.Idtipotramite, t.IdtipotramiteNavigation?.Nombre ?? "", t.IdtipotramiteNavigation?.Estaactivo ?? false),
            new EstadostramiteRecord(t.Idestadotramite, t.IdestadotramiteNavigation?.Nombre ?? "", t.IdestadotramiteNavigation?.Orden ?? 0, t.IdestadotramiteNavigation?.Estaactivo ?? false),
            t.Archivostramites?.Select(a => new ArchivostramiteRecord(a.Id, a.Idarchivo, a.Descripcion, a.Idtramite, a.Estaactivo)).ToList() ?? new List<ArchivostramiteRecord>()
        )).ToList();

        PagedResponseRecord<TramiteRecord> response = new PagedResponseRecord<TramiteRecord>(
            tramiteRecords, pageNumber, pageSize, totalRecords, 
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return ResultResponse<PagedResponseRecord<TramiteRecord>>.Success(response);
    }



    public Task<TramiteRecord> GetTramiteById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TramiteCreateRecord> CreateTramite(TramiteCreateRecord tramiteCreateRecord)
    {
        throw new NotImplementedException();
    }

    public Task<TramiteUpdateRecord> UpdateTramite(TramiteUpdateRecord tramiteUpdateRecord)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTramite(Guid id)
    {
        throw new NotImplementedException();
    }
}