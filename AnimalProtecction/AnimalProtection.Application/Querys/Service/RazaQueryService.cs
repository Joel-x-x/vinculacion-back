using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using AnimalProtection.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtection.Application.Querys.Service
{
    public class RazaQueryService : IRazaQueryService
    {
        public readonly IRazaRepository _razaRepository;

        public RazaQueryService(IRazaRepository razaRepository)
        {
            _razaRepository = razaRepository;
        }

        public async Task<ResultResponse<RazaCreateRecord>> CreateRaza(RazaCreateRecord razaCreateRecord)
        {
            var raza = Raza.CreateFromRecord(razaCreateRecord);
            await _razaRepository.AddAsync(raza);
            await _razaRepository.SaveAsync();

            return ResultResponse<RazaCreateRecord>.Success(razaCreateRecord, 201);
        }

        public async Task<ResultResponse<bool>> DeleteRaza(Guid id)
        {
            var raza = await _razaRepository.GetByIdAsync(id);
            if (raza == null)
            {
                return ResultResponse<bool>.Failure($"No se encontró la raza con el id: {id}", 404);
            }

            raza.Delete();
            await _razaRepository.UpdateAsync(raza);
            return ResultResponse<bool>.Success(true, 200);
        }

        public async Task<ResultResponse<PagedResponseRecord<RazaRecord>>> GetAllRazas(int pageNumber, int pageSize)
        {
            var query = _razaRepository.GetPageableAsync();
            var totalRecords = await query.CountAsync();
            var pagedResult = await query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            var razaRecords = pagedResult.Select(r => new RazaRecord(r)).ToList();
            var response = new PagedResponseRecord<RazaRecord>(
                razaRecords, pageNumber, pageSize, totalRecords,
                (int)Math.Ceiling((double)totalRecords / pageSize)
            );
            return ResultResponse<PagedResponseRecord<RazaRecord>>.Success(response);
        }

        public async Task<ResultResponse<RazaRecord>> GetRazaById<T>(Guid id)
        {
            var raza = await _razaRepository.GetByIdAsync(id);
            if (raza == null)
            {
                return ResultResponse<RazaRecord>.Failure($"No se encontró la raza con el id: {id}", 404);
            }
            RazaRecord razaRecord = new RazaRecord(raza);
            return ResultResponse<RazaRecord>.Success(razaRecord);
        }

        public async Task<ResultResponse<RazaUpdateRecord>> UpdateRaza(RazaUpdateRecord razaUpdateRecord)
        {
            var raza = await _razaRepository.GetByIdAsync(razaUpdateRecord.Id);
            if (raza == null)
            {
                return ResultResponse<RazaUpdateRecord>.Failure($"No se encontró la raza con el id: {razaUpdateRecord.Id}", 404);
            }
            raza.UpdateFromRecord(razaUpdateRecord);
            await _razaRepository.UpdateAsync(raza);
            return ResultResponse<RazaUpdateRecord>.Success(razaUpdateRecord);
        }
    }
}