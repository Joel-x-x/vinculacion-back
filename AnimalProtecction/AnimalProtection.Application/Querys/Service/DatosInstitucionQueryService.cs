using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using EntityFrameworkCore.Paginate;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service;

public class DatosInstitucionService : IDatosInstitucionService
{
    private readonly IDatosInstitucionRepository _datosInstitucionRepository;

    public DatosInstitucionService(IDatosInstitucionRepository datosInstitucionRepository)
    {
        _datosInstitucionRepository = datosInstitucionRepository;
    }

    public async Task<ResultResponse<PagedResponseRecord<DatosInstitucionRecord>>> GetAllDatosInstitucion(int pageNumber, int pageSize)
    {
        IQueryable<DatosInstitucion> query = _datosInstitucionRepository.GetPageableAsync();

        // Obtener el total de registros antes de la paginación
        int totalRecords = await query.CountAsync();

        // Aplicar paginación
        List<DatosInstitucion> pagedResult = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        // Mapear DatosInstitucion a DatosInstitucionRecord
        List<DatosInstitucionRecord> datosInstitucionRecords = pagedResult.Select(d => new DatosInstitucionRecord(d)).ToList();
        PagedResponseRecord<DatosInstitucionRecord> response = new PagedResponseRecord<DatosInstitucionRecord>(
            datosInstitucionRecords, pageNumber, pageSize, totalRecords,
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return ResultResponse<PagedResponseRecord<DatosInstitucionRecord>>.Success(response);
    }

    public async Task<ResultResponse<DatosInstitucionRecord>> GetDatosInstitucionById<T>(Guid id)
    {
        var datosInstitucion = await _datosInstitucionRepository.GetByIdAsync(id);

        if (datosInstitucion == null)
        {
            return ResultResponse<DatosInstitucionRecord>.Failure($"No se encontró la institución con el id: {id}", 404);
        }

        DatosInstitucionRecord datosInstitucionRecord = new DatosInstitucionRecord(datosInstitucion);

        return ResultResponse<DatosInstitucionRecord>.Success(datosInstitucionRecord);
    }

    public async Task<ResultResponse<DatosInstitucionRecord>> CreateDatosInstitucion(DatosInstitucionRecord datosInstitucionRecord)
    {
    var datosInstitucion = new DatosInstitucion
    {
        // Asigna las propiedades del record a la entidad
        Nombre = datosInstitucionRecord.Nombre,
        Direccion = datosInstitucionRecord.Direccion,
        Telefono = datosInstitucionRecord.Telefono,
        // Agrega las demás propiedades necesarias
    };

    await _datosInstitucionRepository.AddAsync(datosInstitucion);
    await _datosInstitucionRepository.SaveChangesAsync();

    return ResultResponse<DatosInstitucionRecord>.Success(new DatosInstitucionRecord(datosInstitucion));
    }

    public async Task<ResultResponse<DatosInstitucionRecord>> UpdateDatosInstitucion(Guid id, DatosInstitucionRecord datosInstitucionRecord)
    {
    var datosInstitucion = await _datosInstitucionRepository.GetByIdAsync(id);

    if (datosInstitucion == null)
    {
        return ResultResponse<DatosInstitucionRecord>.Failure($"No se encontró la institución con el id: {id}", 404);
    }

    // Actualiza las propiedades de la entidad con los valores del record
    datosInstitucion.Nombre = datosInstitucionRecord.Nombre;
    datosInstitucion.Direccion = datosInstitucionRecord.Direccion;
    datosInstitucion.Telefono = datosInstitucionRecord.Telefono;
    // Actualiza las demás propiedades necesarias

    _datosInstitucionRepository.Update(datosInstitucion);
    await _datosInstitucionRepository.SaveChangesAsync();

    return ResultResponse<DatosInstitucionRecord>.Success(new DatosInstitucionRecord(datosInstitucion));
    }

    public async Task<ResultResponse<bool>> DeleteDatosInstitucion(Guid id)
    {
        
    }
}