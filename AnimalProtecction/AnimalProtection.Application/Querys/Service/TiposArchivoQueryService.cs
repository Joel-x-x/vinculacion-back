using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using AnimalProtection.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service
{
    public class TiposArchivoQueryService : ITiposArchivoQueryService
    {
        private readonly ITiposArchivoRepository _repository;

        public TiposArchivoQueryService(ITiposArchivoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultResponse<TiposArchivoCreateRecord>> CreateTiposArchivo(TiposArchivoCreateRecord createRecord)
        {
            var tiposArchivo = new Tiposarchivo
            {
                Id = createRecord.Id ?? Guid.NewGuid(),
                Nombre = createRecord.Nombre,
                Descripcion = createRecord.Descripcion,
                Estaactivo = createRecord.Estaactivo ?? true
            };

            await _repository.AddAsync(tiposArchivo);
            await _repository.SaveAsync();
            return ResultResponse<TiposArchivoCreateRecord>.Success(createRecord, 201);
        }

        public async Task<ResultResponse<bool>> DeleteTiposArchivo(Guid id)
        {
            var tiposArchivo = await _repository.GetByIdAsync(id);
            if (tiposArchivo == null)
            {
                return ResultResponse<bool>.Failure($"No se encontró el tipo de archivo con id: {id}", 404);
            }
            // Delete lógico: marcar como inactivo
            tiposArchivo.Estaactivo = false;
            await _repository.UpdateAsync(tiposArchivo);
            await _repository.SaveAsync();
            return ResultResponse<bool>.Success(true, 200);
        }

        public async Task<ResultResponse<PagedResponseRecord<TiposArchivoRecord>>> GetAllTiposArchivo(int pageNumber, int pageSize)
        {
            var query = _repository.GetPageableAsync()
                .Where(t => t.Estaactivo == true);

            int totalRecords = await query.CountAsync();
            var pagedResult = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var records = pagedResult.Select(t => new TiposArchivoRecord(t)).ToList();
            var response = new PagedResponseRecord<TiposArchivoRecord>(
                records, pageNumber, pageSize, totalRecords,
                (int)Math.Ceiling((double)totalRecords / pageSize)
            );
            return ResultResponse<PagedResponseRecord<TiposArchivoRecord>>.Success(response);
        }

        public async Task<ResultResponse<TiposArchivoRecord>> GetTiposArchivoById<T>(Guid id)
        {
            var tiposArchivo = await _repository.GetByIdAsync(id);
            if (tiposArchivo == null)
            {
                return ResultResponse<TiposArchivoRecord>.Failure($"No se encontró el tipo de archivo con id: {id}", 404);
            }
            return ResultResponse<TiposArchivoRecord>.Success(new TiposArchivoRecord(tiposArchivo));
        }

        public async Task<ResultResponse<TiposArchivoUpdateRecord>> UpdateTiposArchivo(TiposArchivoUpdateRecord updateRecord)
        {
            var tiposArchivo = await _repository.GetByIdAsync(updateRecord.Id);
            if (tiposArchivo == null)
            {
                return ResultResponse<TiposArchivoUpdateRecord>.Failure($"No se encontró el tipo de archivo con id: {updateRecord.Id}", 404);
            }
            // Actualizar propiedades
            tiposArchivo.Nombre = updateRecord.Nombre;
            tiposArchivo.Descripcion = updateRecord.Descripcion;
            if (updateRecord.Estaactivo.HasValue)
                tiposArchivo.Estaactivo = updateRecord.Estaactivo;

            await _repository.UpdateAsync(tiposArchivo);
            await _repository.SaveAsync();
            return ResultResponse<TiposArchivoUpdateRecord>.Success(updateRecord);
        }
    }
}
