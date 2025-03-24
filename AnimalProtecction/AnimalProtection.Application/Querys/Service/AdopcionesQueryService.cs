using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using EntityFrameworkCore.Paginate;
using Microsoft.EntityFrameworkCore;


namespace AnimalProtection.Application.Querys.Service;

public class AdopcionesQueryService : IAdopcionesQueryService
{
    private readonly IAdopcionesRepository _adopcionesRepository;

    public AdopcionesQueryService(IAdopcionesRepository adopcionesRepository)
    {
        _adopcionesRepository = adopcionesRepository;
    }

    public async Task<ResultResponse<PagedResponseRecord<AdopcionesRecord>>> GetAllAdopciones(int pageNumber, int pageSize)
    {
        IQueryable<Adopcione> query = _adopcionesRepository.GetPageableAsync()
        .Where(t => t.Estaactivo == true);

        int totalRecords = await query.CountAsync();

        List<Adopcione> pagedResult = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        List<AdopcionesRecord> adopcionesRecords = pagedResult.Select(t => new AdopcionesRecord(t)).ToList();
        PagedResponseRecord<AdopcionesRecord> response = new PagedResponseRecord<AdopcionesRecord>(
            adopcionesRecords, pageNumber, pageSize, totalRecords,
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return ResultResponse<PagedResponseRecord<AdopcionesRecord>>.Success(response);
    }

    public async Task<ResultResponse<AdopcionesRecord>> GetAdopcionById<T>(Guid id)
    {
        var adopcion = await _adopcionesRepository.GetByIdAsync(id);

        if (adopcion == null)
        {
            return ResultResponse<AdopcionesRecord>.Failure($"No se encontr� la adopcion con el id: {id}", 404);
        }

        AdopcionesRecord adopcionRecord = new AdopcionesRecord(adopcion);

        return ResultResponse<AdopcionesRecord>.Success(adopcionRecord);
    }

    public async Task<ResultResponse<AdopcionesRecord>> CreateAdopcion (AdopcionesCreateRecord adopcionCreateRecord)
    {
        var adopcion = Adopcione.CreateFromRecord(adopcionCreateRecord);

        await _adopcionesRepository.AddAsync(adopcion);
        await _adopcionesRepository.SaveAsync();

        return ResultResponse<AdopcionesRecord>.Success(new AdopcionesRecord(adopcion), 201);
    }

    public async Task<ResultResponse<AdopcionesRecord>> UpdateAdopcion(AdopcionesUpdateRecord adopcionesUpdateRecord)
    {
        var adopcion = await _adopcionesRepository.GetByIdAsync(adopcionesUpdateRecord.Id);

        if(adopcion == null)
        {
            return ResultResponse<AdopcionesRecord>.Failure($"No se encontr� la adopcion con el id: {adopcionesUpdateRecord.Id}", 404);
        }

        adopcion.UpdateFromRecord(adopcionesUpdateRecord);

        await _adopcionesRepository.UpdateAsync(adopcion);

        return ResultResponse<AdopcionesRecord>.Success(new AdopcionesRecord(adopcion), 200);
    }

    public async Task<ResultResponse<bool>> DeleteAdopcion(Guid id)
    {
        var adopcion = await _adopcionesRepository.GetByIdAsync(id);

        if(adopcion == null)
        {
            return ResultResponse<bool>.Failure($"No se encontr� la adopcion con el id: {id}", 404);
        }

        adopcion.eliminar();

        await _adopcionesRepository.UpdateAsync(adopcion);

        return ResultResponse<bool>.Success(true, 200);
    }
}