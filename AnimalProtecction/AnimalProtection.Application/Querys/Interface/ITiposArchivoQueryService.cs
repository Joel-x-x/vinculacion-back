using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface
{
    public interface ITiposArchivoQueryService
    {
        Task<ResultResponse<PagedResponseRecord<TiposArchivoRecord>>> GetAllTiposArchivo(int pageNumber, int pageSize);
        Task<ResultResponse<TiposArchivoRecord>> GetTiposArchivoById<T>(Guid id);
        Task<ResultResponse<TiposArchivoCreateRecord>> CreateTiposArchivo(TiposArchivoCreateRecord createRecord);
        Task<ResultResponse<TiposArchivoUpdateRecord>> UpdateTiposArchivo(TiposArchivoUpdateRecord updateRecord);
        Task<ResultResponse<bool>> DeleteTiposArchivo(Guid id);
    }
}
