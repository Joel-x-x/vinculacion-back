using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface ITipostramiteQueryService
{
    Task<ResultResponse<PagedResponseRecord<TiposTramiteRecord>>> GetAllTipostramite(int pageNumber, int pageSize);
    Task<ResultResponse<TiposTramiteRecord>> GetTipostramiteById<T>(Guid id);
    Task<ResultResponse<TiposTramiteRecord>> CreateTipostramite(TiposTramiteCreateRecord tipostramiteCreateRecord);
    Task<ResultResponse<TiposTramiteRecord>> UpdateTipostramite(TiposTramiteUpdateRecord tipostramiteUpdateRecord);
    Task<ResultResponse<bool>> DeleteTipostramite(Guid id);
}