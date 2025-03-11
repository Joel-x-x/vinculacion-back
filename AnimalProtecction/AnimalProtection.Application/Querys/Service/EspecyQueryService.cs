using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using AnimalProtection.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service
{
    public class EspecyQueryService : IEspecyQueryService
    {
        public readonly IEspecyRepository _especyRepository;

        public EspecyQueryService(IEspecyRepository especyRepository)
        {
            _especyRepository = especyRepository;
        }

        public async Task<ResultResponse<EspecyCreateRecord>> CreateEspecy(EspecyCreateRecord especyCreateRecord)
        {
            var especy = Especy.CreateFromRecord(especyCreateRecord);
            await _especyRepository.AddAsync(especy);
            await _especyRepository.SaveAsync();
            return ResultResponse<EspecyCreateRecord>.Success(especyCreateRecord, 201);
        }

        public async Task<ResultResponse<bool>> DeleteEspecy(Guid id)
        {
            var especy = await _especyRepository.GetByIdAsync(id);
            if (especy == null)
            {
                return ResultResponse<bool>.Failure($"No se encontró la especie con el id: {id}", 404);
            }

            especy.Delete();
            await _especyRepository.UpdateAsync(especy);
            await _especyRepository.SaveAsync();
            return ResultResponse<bool>.Success(true, 200);
        }

        public async Task<ResultResponse<PagedResponseRecord<EspecyRecord>>> GetAllEspecies(int pageNumber, int pageSize)
        {
            var query = _especyRepository.GetPageableAsync();
            var totalRecords = await query.CountAsync();
            var pagedResult = await query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            var especyRecords = pagedResult.Select(e => new EspecyRecord(e)).ToList();
            var response = new PagedResponseRecord<EspecyRecord>(
                especyRecords, pageNumber, pageSize, totalRecords,
                (int)Math.Ceiling((double)totalRecords / pageSize)
            );
            return ResultResponse<PagedResponseRecord<EspecyRecord>>.Success(response);
        }

        public async Task<ResultResponse<EspecyRecord>> GetEspecyById<T>(Guid id)
        {
            var especy = await _especyRepository.GetByIdAsync(id);
            if (especy == null)
            {
                return ResultResponse<EspecyRecord>.Failure($"No se encontró la especie con el id: {id}", 404);
            }
            var especyRecord = new EspecyRecord(especy);
            return ResultResponse<EspecyRecord>.Success(especyRecord);
        }

        public async Task<ResultResponse<EspecyUpdateRecord>> UpdateEspecy(EspecyUpdateRecord especyUpdateRecord)
        {
            var especy = _especyRepository.GetByIdAsync(especyUpdateRecord.Id);
            if (especy == null)
            {
                return ResultResponse<EspecyUpdateRecord>.Failure($"No se encontró la especie con el id: {especyUpdateRecord.Id}", 404);
            }

            especy.Result.UpdateFromRecord(especyUpdateRecord);
            await _especyRepository.UpdateAsync(especy.Result);
            await _especyRepository.SaveAsync();

            return ResultResponse<EspecyUpdateRecord>.Success(especyUpdateRecord);
        }
    }
}