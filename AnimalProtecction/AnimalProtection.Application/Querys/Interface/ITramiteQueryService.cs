using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface ITramiteQueryService
{
    Task<ResultResponse<PagedResponseRecord<TramiteRecord>>> GetAllTramite(int pageNumber, int pageSize);
    Task<ResultResponse<TramiteRecord>> GetTramiteById<T>(Guid id);
    Task<ResultResponse<TramiteCreateRecord>> CreateTramite(TramiteCreateRecord tramiteCreateRecord);
    Task<ResultResponse<TramiteUpdateRecord>> UpdateTramite(TramiteUpdateRecord tramiteUpdateRecord);
    Task<ResultResponse<bool>> DeleteTramite(Guid id);
}