using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface ITiposCampanaQueryService
{
    Task<ResultResponse<List<TiposCampanaRecord>>> GetAll();
    Task<ResultResponse<TiposCampanaRecord>> GetById(Guid id);
    Task<ResultResponse<TiposCampanaRecord>> Create(TiposCampanaCreateRecord createRecord);
    Task<ResultResponse<TiposCampanaRecord>> Update(Guid id, TiposCampanaUpdateRecord updateRecord);
    Task<ResultResponse<bool>> Delete(Guid id);
} 