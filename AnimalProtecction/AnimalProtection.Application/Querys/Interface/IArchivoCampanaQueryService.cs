using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface IArchivoCampanaQueryService
{
    Task<ResultResponse<List<ArchivoCampanaRecord>>> GetArchivosByCampanaId(Guid campanaId);
    Task<ResultResponse<ArchivoCampanaRecord>> GetArchivoCampanaById(Guid id);
    Task<ResultResponse<ArchivoCampanaRecord>> CreateArchivoCampana(ArchivoCampanaCreateRecord createRecord);
    Task<ResultResponse<ArchivoCampanaRecord>> UpdateArchivoCampana(Guid id, ArchivoCampanaUpdateRecord updateRecord);
    Task<ResultResponse<bool>> DeleteArchivoCampana(Guid id);
} 