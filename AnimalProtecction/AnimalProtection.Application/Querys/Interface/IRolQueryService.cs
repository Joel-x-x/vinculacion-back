using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Service;

public interface IRolService
{
    Task<ResultResponse<PagedResponseRecord<RolRecord>>> GetAllRol(int pageNumber, int pageSize);
    Task<ResultResponse<RolRecord>> GetRolById<T>(Guid id);
    Task<ResultResponse<RolRecord>> CreateRol(RolCreateRecord rolCreateRecord);
    Task<ResultResponse<RolRecord>> UpdateRol(RolUpdateRecord rolUpdateRecord);
    Task<ResultResponse<bool>> DeleteRol(Guid id);
}