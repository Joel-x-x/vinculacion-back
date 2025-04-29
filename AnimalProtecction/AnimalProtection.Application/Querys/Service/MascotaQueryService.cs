using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using AnimalProtection.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service
{
    public class MascotaQueryService : IMascotaQueryService
    {
        public readonly IMascotaRepository _mascotaRepository;

        public MascotaQueryService(IMascotaRepository mascotaRepository)
        {
            _mascotaRepository = mascotaRepository;
        }

        public async Task<ResultResponse<MascotaCreateRecord>> CreateMascota(MascotaCreateRecord mascotaCreateRecord)
        {
            var mascota = Mascota.CreateFromRecord(mascotaCreateRecord);
            await _mascotaRepository.AddAsync(mascota);
            await _mascotaRepository.SaveAsync();
            return ResultResponse<MascotaCreateRecord>.Success(mascotaCreateRecord, 201);
        }

        public async Task<ResultResponse<bool>> DeleteMascota(Guid id)
        {
            var mascota = await _mascotaRepository.GetByIdAsync(id);
            if (mascota == null)
            {
                return ResultResponse<bool>.Failure($"No se encontró la mascota con el id: {id}", 404);
            }
            mascota.Delete();
            await _mascotaRepository.UpdateAsync(mascota);
            return ResultResponse<bool>.Success(true, 200);
        }

        public async Task<ResultResponse<PagedResponseRecord<MascotaRecord>>> GetAllMascotas(int pageNumber, int pageSize)
        {
            var query = _mascotaRepository.GetPageableAsync().Where(t => t.Estaactivo == true);
            var totalRecords = await query.CountAsync();
            var pagedResult = await query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            var mascotaRecords = pagedResult.Select(m => new MascotaRecord(m)).ToList();
            var response = new PagedResponseRecord<MascotaRecord>(
                mascotaRecords, pageNumber, pageSize, totalRecords,
                (int)Math.Ceiling((double)totalRecords / pageSize)
            );
            return ResultResponse<PagedResponseRecord<MascotaRecord>>.Success(response);
        }

        public async Task<ResultResponse<MascotaRecord>> GetMascotaById<T>(Guid id)
        {
            var mascota = await _mascotaRepository.GetByIdAsync(id);
            if (mascota == null)
            {
                return ResultResponse<MascotaRecord>.Failure($"No se encontró la mascota con el id: {id}", 404);
            }
            MascotaRecord mascotaRecord = new MascotaRecord(mascota);
            return ResultResponse<MascotaRecord>.Success(mascotaRecord);
        }

        public async Task<ResultResponse<MascotaUpdateRecord>> UpdateMascota(MascotaUpdateRecord mascotaUpdateRecord)
        {
            var mascota = await _mascotaRepository.GetByIdAsync(mascotaUpdateRecord.Id);
            if (mascota == null)
            {
                return ResultResponse<MascotaUpdateRecord>.Failure($"No se encontró la mascota con el id: {mascotaUpdateRecord.Id}", 404);
            }
            mascota.UpdateFromRecord(mascotaUpdateRecord);
            await _mascotaRepository.UpdateAsync(mascota);
            return ResultResponse<MascotaUpdateRecord>.Success(mascotaUpdateRecord);
        }
    }
}