using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface ICampanaQueryService
{
    Task<ResultResponse<PagedResponseRecord<CampanaRecord>>> GetAllCampanas(int pageNumber, int pageSize);
    Task<ResultResponse<T>> GetCampanaById<T>(Guid id) where T : class;
    Task<ResultResponse<CampanaRecord>> CreateCampana(CampanaCreateRecord campanaCreateRecord);
    Task<ResultResponse<CampanaRecord>> UpdateCampana(Guid id, CampanaUpdateRecord campanaUpdateRecord);
    Task<ResultResponse<bool>> DeleteCampana(Guid id);
} 