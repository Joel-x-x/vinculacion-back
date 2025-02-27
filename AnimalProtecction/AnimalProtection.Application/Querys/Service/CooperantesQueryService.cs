using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using EntityFrameworkCore.Paginate;
using Microsoft.EntityFrameworkCore;


namespace AnimalProtection.Application.Querys.Service;

public class CooperantesQueryService : ICooperantesQueryService
{
    private readonly ICooperantesRepository _cooperantesRepository;

    public CooperantesQueryService(ICooperantesRepository cooperantesRepository)
    {
        _cooperantesRepository = cooperantesRepository;
    }

    public async Task<ResultResponse<PagedResponseRecord<CooperantesRecord>>> GetAllCooperantes(int pageNumber, int pageSize)
    {
        IQueryable<Cooperantes> query = _cooperantesRepository.GetPageableAsync();

        int totalRecords = await query.CountAsync();

        List<Cooperantes> pagedResult = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        List<CooperantesRecord> cooperantesRecords = pagedResult.Select(t => new CooperantesRecord(t)).ToList();
        PagedResponseRecord<CooperantesRecord> response = new PagedResponseRecord<CooperantesRecord>(
            cooperantesRecords, pageNumber, pageSize, totalRecords,
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return ResultResponse<PagedResponseRecord<CooperantesRecord>>.Success(response);
    }

    public async Task<ResultResponse<CooperantesRecord>> GetCooperanteById<T>(Guid id)
    {
        var cooperante = await _cooperantesRepository.GetByIdAsync(id);

        if (cooperante == null)
        {
            return ResultResponse<CooperantesRecord>.Failure($"No se encontró el cooperante con el id: {id}", 404);
        }

        CooperantesRecord cooperanteRecord = new CooperantesRecord(cooperante);

        return ResultResponse<CooperantesRecord>.Success(cooperanteRecord);
    }

    public async Task<ResultResponse<CooperantesCreateRecord>> CreateCooperante (CooperantesCreateRecord cooperantesCreateRecord)
    {
        if(cooperantesCreateRecord == null)
        {
            return ResultResponse<CooperantesCreateRecord>.Failure("Los datos del cooperante no son válidos", 400);
        }

        var cooperante = Cooperantes.CreateFromRecord(cooperantesCreateRecord);

        await _cooperantesRepository.AddAsync(cooperante);
        await _cooperantesRepository.SaveAsync();

        return ResultResponse<CooperantesCreateRecord>.Success(cooperantesCreateRecord, 201);
    }

    public async Task<ResultResponse<CooperantesUpdateRecord>> UpdateCooperante(CooperantesUpdateRecord cooperantesUpdateRecord)
    {
        var cooperante = await _cooperantesRepository.GetByIdAsync(cooperantesUpdateRecord.Id);

        if(cooperante == null)
        {
            return ResultResponse<CooperantesUpdateRecord>.Failure($"No se encontró el cooperante con el id: {cooperantesUpdateRecord.Id}", 404);
        }

        cooperante.UpdateFromRecord(cooperantesUpdateRecord);

        await _cooperantesRepository.UpdateAsync(cooperante);

        return ResultResponse<CooperantesUpdateRecord>.Success(cooperantesUpdateRecord, 200);
    }

    public async Task<ResultResponse<bool>> DeleteCooperante(Guid id)
    {
        var cooperante = await _cooperantesRepository.GetByIdAsync(id);

        if(cooperante == null)
        {
            return ResultResponse<bool>.Failure($"No se encontró el cooperante con el id: {id}", 404);
        }

        cooperante.eliminar();

        await _cooperantesRepository.UpdateAsync(cooperante);

        return ResultResponse<bool>.Success(true, 200);
    }
}