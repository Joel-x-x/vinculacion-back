using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface ITramiteQueryService
{
    Task<ResultResponse<PagedResponseRecord<TramiteRecord>>> GetAllTramite(int pageNumber, int pageSize);
    Task<TramiteRecord> GetTramiteById(Guid id);
    Task<TramiteCreateRecord> CreateTramite(TramiteCreateRecord tramiteCreateRecord);
    Task<TramiteUpdateRecord> UpdateTramite(TramiteUpdateRecord tramiteUpdateRecord);
    Task<bool> DeleteTramite(Guid id);
}