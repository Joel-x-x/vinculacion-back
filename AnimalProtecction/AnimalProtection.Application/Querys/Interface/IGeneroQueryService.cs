using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface IGeneroQueryService
{
    Task<ResultResponse<PagedResponseRecord<GeneroRecord>>> GetAllGeneros(int pageNumber, int pageSize);

    Task<ResultResponse<GeneroRecord>> GetGeneroById<T>(Guid id);

    Task<ResultResponse<GeneroCreateRecord>> CreateGenero(GeneroCreateRecord generoCreateRecord);

    Task<ResultResponse<GeneroUpdateRecord>> UpdateGenero(GeneroUpdateRecord generoUpdateRecord);

    Task<ResultResponse<bool>> DeleteGenero(Guid id);
}