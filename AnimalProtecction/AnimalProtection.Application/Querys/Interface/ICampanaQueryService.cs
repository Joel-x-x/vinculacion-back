using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface ICampanaQueryService
{
    Task<Result<PagedResult<CampanaRecord>>> GetAllCampanas(int pageNumber, int pageSize);
    Task<Result<T>> GetCampanaById<T>(Guid id) where T : class;
    Task<Result<CampanaRecord>> CreateCampana(CampanaCreateRecord campanaCreateRecord);
    Task<Result<CampanaRecord>> UpdateCampana(Guid id, CampanaUpdateRecord campanaUpdateRecord);
    Task<Result<bool>> DeleteCampana(Guid id);
} 