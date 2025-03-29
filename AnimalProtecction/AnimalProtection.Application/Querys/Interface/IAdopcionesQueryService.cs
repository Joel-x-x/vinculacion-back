using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface IAdopcionesQueryService
{
    Task<ResultResponse<PagedResponseRecord<AdopcionesRecord>>> GetAllAdopciones(int pageNumber, int pageSize);
    Task<ResultResponse<AdopcionesRecord>> GetAdopcionById<T>(Guid id);
    Task<ResultResponse<AdopcionesRecord>> CreateAdopcion(AdopcionesCreateRecord adopcionesCreateRecord);
    Task<ResultResponse<AdopcionesRecord>> UpdateAdopcion(AdopcionesUpdateRecord adopcionesUpdateRecord);
    Task<ResultResponse<bool>> DeleteAdopcion(Guid id);

}