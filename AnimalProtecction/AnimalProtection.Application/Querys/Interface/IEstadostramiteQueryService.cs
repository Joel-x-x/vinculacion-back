using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface IEstadostramiteQueryService
{
    Task<ResultResponse<PagedResponseRecord<EstadostramiteRecord>>> GetAllEstadostramite(int pageNumber, int pageSize);
    Task<ResultResponse<EstadostramiteRecord>> GetEstadostramiteById<T>(Guid id);
    Task<ResultResponse<EstadostramiteRecord>> CreateEstadostramite(EstadostramiteCreateRecord estadostramiteCreateRecord);
    Task<ResultResponse<EstadostramiteRecord>> UpdateEstadostramite(EstadostramiteUpdateRecord estadostramiteUpdateRecord);
    Task<ResultResponse<bool>> DeleteEstadostramite(Guid id);
}