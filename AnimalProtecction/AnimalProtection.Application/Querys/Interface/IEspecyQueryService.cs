using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface
{
    public interface IEspecyQueryService
    {
        Task<ResultResponse<PagedResponseRecord<EspecyRecord>>> GetAllEspecies(int pageNumber, int pageSize);

        Task<ResultResponse<EspecyRecord>> GetEspecyById<T>(Guid id);

        Task<ResultResponse<EspecyCreateRecord>> CreateEspecy(EspecyCreateRecord especyCreateRecord);

        Task<ResultResponse<EspecyUpdateRecord>> UpdateEspecy(EspecyUpdateRecord especyUpdateRecord);

        Task<ResultResponse<bool>> DeleteEspecy(Guid id);
    }
}