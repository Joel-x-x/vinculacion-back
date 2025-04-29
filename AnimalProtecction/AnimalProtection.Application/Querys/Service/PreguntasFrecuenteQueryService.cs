using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using EntityFrameworkCore.Paginate;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service;

public class PreguntasFrecuenteQueryService : IPreguntasFrecuenteQueryService
{

    private readonly IPreguntasFrecuenteRepository _preguntasFrecuenteRepository;
    
    public PreguntasFrecuenteQueryService(IPreguntasFrecuenteRepository preguntasFrecuenteRepository)
    {
        _preguntasFrecuenteRepository = preguntasFrecuenteRepository;
    }
    
    public async Task<ResultResponse<PagedResponseRecord<PreguntasFrecuenteRecord>>> GetAllPreguntasFrecuente(int pageNumber, int pageSize)
    {
        IQueryable<Preguntasfrecuente> query = _preguntasFrecuenteRepository.GetPageableAsync()
        .Where(t => t.Estaactivo == true);

        // Obtener el total de registros antes de la paginación
        int totalRecords = await query.CountAsync();

        // Aplicar paginación
        List<Preguntasfrecuente> pagedResult = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        // Mapear Preguntas Frecuentes a Record
        List<PreguntasFrecuenteRecord> preguntasFrecuentesRecords = pagedResult.Select(t => new PreguntasFrecuenteRecord(t)).ToList();
        PagedResponseRecord<PreguntasFrecuenteRecord> response = new PagedResponseRecord<PreguntasFrecuenteRecord>(
            preguntasFrecuentesRecords, pageNumber, pageSize, totalRecords, 
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return ResultResponse<PagedResponseRecord<PreguntasFrecuenteRecord>>.Success(response);
    }

    public async Task<ResultResponse<PreguntasFrecuenteRecord>> GetPreguntasFrecuenteById<T>(Guid id)
    {
        var preguntaFrecuente = await _preguntasFrecuenteRepository.GetByIdAsync(id);

        if (preguntaFrecuente == null)
        {
            return ResultResponse<PreguntasFrecuenteRecord>.Failure($"No se encontró el la pregunta con el id: {id}", 404);
        }

        PreguntasFrecuenteRecord preguntasFrecuenteRecord = new PreguntasFrecuenteRecord(preguntaFrecuente);

        return ResultResponse<PreguntasFrecuenteRecord>.Success(preguntasFrecuenteRecord);
    }

    public async Task<ResultResponse<PreguntasFrecuenteRecord>> CreatePreguntasFrecuente(PreguntasFrecuenteCreateRecord preguntasFrecuenteCreateRecord)
    {
        // TODO: Validar los datos del trámite
        if (preguntasFrecuenteCreateRecord == null)
        {
            return ResultResponse<PreguntasFrecuenteRecord>.Failure("Los datos de la pregunta son inválidos", 400);
        }

        // Crear una nueva instancia de Pregunta frecuente usando el método de fábrica
        var preguntaFrecuente = Preguntasfrecuente.CreateFromRecord(preguntasFrecuenteCreateRecord);

        // Guardar el trámite en la base de datos
        await _preguntasFrecuenteRepository.AddAsync(preguntaFrecuente);
        await _preguntasFrecuenteRepository.SaveAsync();

        // Devolver el trámite creado como respuesta
        return ResultResponse<PreguntasFrecuenteRecord>.Success(new PreguntasFrecuenteRecord(preguntaFrecuente), 201);
    }

    public async Task<ResultResponse<PreguntasFrecuenteRecord>> UpdatePreguntasFrecuente(PreguntasFrecuenteUpdateRecord preguntasFrecuenteUpdateRecord)
    {
        // TODO: Validar los datos del trámite
        var preguntaFrecuente = await _preguntasFrecuenteRepository.GetByIdAsync(preguntasFrecuenteUpdateRecord.Id);

        if (preguntaFrecuente == null)
        {
            return ResultResponse<PreguntasFrecuenteRecord>.Failure($"No se encontró el trámite con el id: {preguntasFrecuenteUpdateRecord.Id}", 404);
        }

        preguntaFrecuente.UpdateFromRecord(preguntasFrecuenteUpdateRecord);

        await _preguntasFrecuenteRepository.UpdateAsync(preguntaFrecuente);

        return ResultResponse<PreguntasFrecuenteRecord>.Success(new PreguntasFrecuenteRecord(preguntaFrecuente), 200);
    }

    public async Task<ResultResponse<bool>> DeletePreguntasFrecuente(Guid id)
    {
        var preguntaFrecuente = await _preguntasFrecuenteRepository.GetByIdAsync(id);

        if (preguntaFrecuente == null)
        {
            return ResultResponse<bool>.Failure($"No se encontró la pregunta con el id: {id}", 404);
        }

        preguntaFrecuente.eliminar();

        await _preguntasFrecuenteRepository.UpdateAsync(preguntaFrecuente);

        return ResultResponse<bool>.Success(true, 200);
    }






}
