using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface
{
    public interface IMascotaQueryService
    {
        Task<ResultResponse<PagedResponseRecord<MascotaRecord>>> GetAllMascotas(int pageNumber, int pageSize);

        Task<ResultResponse<MascotaRecord>> GetMascotaById<T>(Guid id);

        Task<ResultResponse<MascotaCreateRecord>> CreateMascota(MascotaCreateRecord mascotaCreateRecord);

        Task<ResultResponse<MascotaUpdateRecord>> UpdateMascota(MascotaUpdateRecord mascotaUpdateRecord);

        Task<ResultResponse<bool>> DeleteMascota(Guid id);
    }
}