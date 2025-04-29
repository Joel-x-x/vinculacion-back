using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface IPreguntasFrecuenteQueryService
{
    Task<ResultResponse<PagedResponseRecord<PreguntasFrecuenteRecord>>> GetAllPreguntasFrecuente(int pageNumber, int pageSize);
    Task<ResultResponse<PreguntasFrecuenteRecord>> GetPreguntasFrecuenteById<T>(Guid id);
    Task<ResultResponse<PreguntasFrecuenteRecord>> CreatePreguntasFrecuente(PreguntasFrecuenteCreateRecord preguntasFrecuenteCreateRecord);
    Task<ResultResponse<PreguntasFrecuenteRecord>> UpdatePreguntasFrecuente(PreguntasFrecuenteUpdateRecord preguntasFrecuenteUpdateRecord);
    Task<ResultResponse<bool>> DeletePreguntasFrecuente(Guid id);
}
