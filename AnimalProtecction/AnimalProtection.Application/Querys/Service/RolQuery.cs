using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.Application.Querys.Interface;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;
using EntityFrameworkCore.Paginate;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service;

public class RolService : IRolService{

    private readonly IRolRepository _rolRepository;

    public RolService(IRolRepository rolRepository)
    {
        _rolRepository = rolRepository;
    }

    public async Task<ResultResponse<PagedResponseRecord<RolRecord>>> GetAllRol(int pageNumber, int pageSize)
    {
        IQueryable<Rol> query = _rolRepository.GetPageableAsync();

        // Obtener el total de registros antes de la paginación
        int totalRecords = await query.CountAsync();

        // Aplicar paginación

        List<Rol> pagedResult = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        // Mapear Rol a RolRecord
        List<RolRecord> rolRecords = pagedResult.Select(r => new RolRecord(r)).ToList();
        PagedResponseRecord<RolRecord> response = new PagedResponseRecord<RolRecord>(
            rolRecords, pageNumber, pageSize, totalRecords,
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return ResultResponse<PagedResponseRecord<RolRecord>>.Success(response);
    }

    public async Task<ResultResponse<RolRecord>> GetRolById<T>(Guid id)
    {
        var rol = await _rolRepository.GetByIdAsync(id);

        if (rol == null)
        {
            return ResultResponse<RolRecord>.Failure($"No se encontró el rol con el id: {id}", 404);
        }

        RolRecord rolRecord = new RolRecord(rol);

        return ResultResponse<RolRecord>.Success(rolRecord);
    }

    public async Task<ResultResponse<RolRecord>> CreateRol(RolCreateRecord rolRecord)
    {
        var rol = Rol.CreateFromRecord(rolRecord);

        await _rolRepository.AddAsync(rol);
        await _rolRepository.SaveChangesAsync();

        return ResultResponse<RolRecord>.Success(new RolRecord(rol));
    }

    public async Task<ResultResponse<RolRecord>> UpdateRol(RolUpdateRecord rolRecord)
    {
        var rol = await _rolRepository.GetByIdAsync(rolRecord.Id);

        if (rol == null)
        {
            return ResultResponse<RolRecord>.Failure($"No se encontró el rol con el id: {rolRecord.Id}", 404);
        }

        rol.UpdateFromRecord(rolRecord);

        await _rolRepository.UpdateAsync(rol);
        await _rolRepository.SaveAsync();

        return ResultResponse<RolRecord>.Success(new RolRecord(rol));
    }

    public Task<ResultResponse<bool>> DeleteRol(Guid id)
    {
        throw new NotImplementedException();
    }


}