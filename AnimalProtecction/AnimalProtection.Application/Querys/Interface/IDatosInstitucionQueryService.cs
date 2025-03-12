using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Service;

public interface IDatosInstitucionService
{
    Task<ResultResponse<PagedResponseRecord<DatosInstitucionRecord>>> GetAllDatosInstitucion(int pageNumber, int pageSize);
    Task<ResultResponse<DatosInstitucionRecord>> GetDatosInstitucionById<T>(Guid id);
    Task<ResultResponse<DatosInstitucionRecord>> CreateDatosInstitucion(DatosInstitucionCreateRecord datosInstitucionCreateRecord);
    Task<ResultResponse<DatosInstitucionRecord>> UpdateDatosInstitucion(DatosInstitucionUpdateRecord datosInstitucionUpdateRecord);
    Task<ResultResponse<bool>> DeleteDatosInstitucion(Guid id);
}