using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface ICooperantesQueryService
{
    Task<ResultResponse<PagedResponseRecord<CooperantesRecord>>> GetAllCooperantes(int pageNumber, int pageSize);
    Task<ResultResponse<CooperantesRecord>> GetCooperanteById<T>(Guid id);
    Task<ResultResponse<CooperantesRecord>> CreateCooperante(CooperantesCreateRecord cooperantesCreateRecord);
    Task<ResultResponse<CooperantesRecord>> UpdateCooperante(CooperantesUpdateRecord cooperantesUpdateRecord);
    Task<ResultResponse<bool>> DeleteCooperante(Guid id);

}