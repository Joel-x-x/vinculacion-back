using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using AnimalProtection.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service
{
    public class ArchivoQueryService : IArchivoQueryService
    {
        private readonly IArchivoRepository _repository;

        public ArchivoQueryService(IArchivoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultResponse<ArchivoCreateRecord>> CreateArchivo(ArchivoCreateRecord createRecord)
        {
            var archivo = new Archivo
            {
                Id = createRecord.Id ?? Guid.NewGuid(),
                Url = createRecord.Url,
                Formato = createRecord.Formato,
                Idtipoarchivo = createRecord.Idtipoarchivo,
                Estaactivo = createRecord.Estaactivo ?? true
            };

            await _repository.AddAsync(archivo);
            await _repository.SaveAsync();
            return ResultResponse<ArchivoCreateRecord>.Success(createRecord, 201);
        }

        public async Task<ResultResponse<bool>> DeleteArchivo(Guid id)
        {
            var archivo = await _repository.GetByIdAsync(id);
            if (archivo == null)
            {
                return ResultResponse<bool>.Failure($"No se encontró el archivo con id: {id}", 404);
            }
            // Soft delete
            archivo.Estaactivo = false;
            await _repository.UpdateAsync(archivo);
            await _repository.SaveAsync();
            return ResultResponse<bool>.Success(true, 200);
        }

        public async Task<ResultResponse<PagedResponseRecord<ArchivoRecord>>> GetAllArchivos(int pageNumber, int pageSize)
        {
            // Filtra por Estaactivo = true
            var query = _repository.GetPageableAsync()
                .Where(a => a.Estaactivo == true);

            var totalRecords = await query.CountAsync();
            var pagedResult = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var records = pagedResult.Select(a => new ArchivoRecord(a)).ToList();
            var response = new PagedResponseRecord<ArchivoRecord>(
                records,
                pageNumber,
                pageSize,
                totalRecords,
                (int)Math.Ceiling((double)totalRecords / pageSize)
            );

            return ResultResponse<PagedResponseRecord<ArchivoRecord>>.Success(response);
        }

        public async Task<ResultResponse<ArchivoRecord>> GetArchivoById<T>(Guid id)
        {
            var archivo = await _repository.GetByIdAsync(id);
            if (archivo == null)
            {
                return ResultResponse<ArchivoRecord>.Failure($"No se encontró el archivo con id: {id}", 404);
            }
            return ResultResponse<ArchivoRecord>.Success(new ArchivoRecord(archivo));
        }

        public async Task<ResultResponse<ArchivoUpdateRecord>> UpdateArchivo(ArchivoUpdateRecord updateRecord)
        {
            var archivo = await _repository.GetByIdAsync(updateRecord.Id);
            if (archivo == null)
            {
                return ResultResponse<ArchivoUpdateRecord>.Failure($"No se encontró el archivo con id: {updateRecord.Id}", 404);
            }
            // Actualiza campos
            archivo.Url = updateRecord.Url;
            archivo.Formato = updateRecord.Formato;
            archivo.Idtipoarchivo = updateRecord.Idtipoarchivo;
            if (updateRecord.Estaactivo.HasValue)
                archivo.Estaactivo = updateRecord.Estaactivo;

            await _repository.UpdateAsync(archivo);
            await _repository.SaveAsync();
            return ResultResponse<ArchivoUpdateRecord>.Success(updateRecord);
        }
    }
}
