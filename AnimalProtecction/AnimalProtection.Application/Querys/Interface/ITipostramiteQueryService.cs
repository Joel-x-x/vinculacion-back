using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface ITipostramiteQueryService
{
    Task<ResultResponse<PagedResponseRecord<TipostramiteRecord>>> GetAllTipostramite(int pageNumber, int pageSize);
    Task<ResultResponse<TipostramiteRecord>> GetTipostramiteById<T>(Guid id);
    Task<ResultResponse<TipostramiteRecord>> CreateTipostramite(TipostramiteCreateRecord tipostramiteCreateRecord);
    Task<ResultResponse<TipostramiteRecord>> UpdateTipostramite(TipostramiteUpdateRecord tipostramiteUpdateRecord);
    Task<ResultResponse<bool>> DeleteTipostramite(Guid id);
}