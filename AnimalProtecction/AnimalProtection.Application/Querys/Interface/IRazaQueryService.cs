using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface
{
    public interface IRazaQueryService
    {
        Task<ResultResponse<PagedResponseRecord<RazaRecord>>> GetAllRazas(int pageNumber, int pageSize);

        Task<ResultResponse<RazaRecord>> GetRazaById<T>(Guid id);

        Task<ResultResponse<RazaCreateRecord>> CreateRaza(RazaCreateRecord razaCreateRecord);

        Task<ResultResponse<RazaUpdateRecord>> UpdateRaza(RazaUpdateRecord razaUpdateRecord);

        Task<ResultResponse<bool>> DeleteRaza(Guid id);
    }
}