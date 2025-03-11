using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using AnimalProtection.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service;

public class GeneroQueryService : IGeneroQueryService
{
    public readonly IGeneroRepository _generoRepository;

    public GeneroQueryService(IGeneroRepository generoRepository)
    {
        _generoRepository = generoRepository;
    }

    public async Task<ResultResponse<GeneroCreateRecord>> CreateGenero(GeneroCreateRecord generoCreateRecord)
    {
        var genero = Genero.CreateFromRecord(generoCreateRecord);
        await _generoRepository.AddAsync(genero);
        await _generoRepository.SaveAsync();

        return ResultResponse<GeneroCreateRecord>.Success(generoCreateRecord, 201);
    }

    public async Task<ResultResponse<bool>> DeleteGenero(Guid id)
    {
        var genero = await _generoRepository.GetByIdAsync(id);
        if (genero == null)
        {
            return ResultResponse<bool>.Failure($"No se encontró el género con el id: {id}", 404);
        }

        genero.Delete();
        await _generoRepository.UpdateAsync(genero);
        return ResultResponse<bool>.Success(true, 200);
    }

    public async Task<ResultResponse<PagedResponseRecord<GeneroRecord>>> GetAllGeneros(int pageNumber, int pageSize)
    {
        IQueryable<Genero> query = _generoRepository.GetPageableAsync();

        int totalRecords = await query.CountAsync();

        List<Genero> pagedResult = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        List<GeneroRecord> generoRecords = pagedResult.Select(g => new GeneroRecord(g)).ToList();
        PagedResponseRecord<GeneroRecord> response = new PagedResponseRecord<GeneroRecord>(
            generoRecords, pageNumber, pageSize, totalRecords,
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return ResultResponse<PagedResponseRecord<GeneroRecord>>.Success(response);
    }

    public async Task<ResultResponse<GeneroRecord>> GetGeneroById<T>(Guid id)
    {
        var genero = await _generoRepository.GetByIdAsync(id);
        if (genero == null)
        {
            return ResultResponse<GeneroRecord>.Failure($"No se encontró el género con el id: {id}", 404);
        }
        GeneroRecord generoRecord = new GeneroRecord(genero);
        return ResultResponse<GeneroRecord>.Success(generoRecord);
    }

    public async Task<ResultResponse<GeneroUpdateRecord>> UpdateGenero(GeneroUpdateRecord generoUpdateRecord)
    {
        var genero = await _generoRepository.GetByIdAsync(generoUpdateRecord.Id);
        if (genero == null)
        {
            return ResultResponse<GeneroUpdateRecord>.Failure($"No se encontró el género con el id: {generoUpdateRecord.Id}", 404);
        }
        genero.UpdateFromRecord(generoUpdateRecord);
        await _generoRepository.UpdateAsync(genero);
        return ResultResponse<GeneroUpdateRecord>.Success(generoUpdateRecord);
    }
}