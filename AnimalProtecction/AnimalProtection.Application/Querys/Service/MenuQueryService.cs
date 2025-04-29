using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Domain.Result;
using EntityFrameworkCore.Paginate;
using Microsoft.EntityFrameworkCore;


namespace AnimalProtection.Application.Querys.Service;

public class MenuQueryService : IMenuQueryService
{
    private readonly IMenuRepository _menuRepository;

    public MenuQueryService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ResultResponse<PagedResponseRecord<MenuRecord>>> GetAllMenus(int pageNumber, int pageSize)
    {
        IQueryable<Menu> query = _menuRepository.GetPageableAsync()
        .Where(t => t.Estaactivo == true);

        int totalRecords = await query.CountAsync();

        List<Menu> pagedResult = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        List<MenuRecord> menuRecords = pagedResult.Select(t => new MenuRecord(t)).ToList();
        PagedResponseRecord<MenuRecord> response = new PagedResponseRecord<MenuRecord>(
            menuRecords, pageNumber, pageSize, totalRecords,
            (int)Math.Ceiling((double)totalRecords / pageSize)
        );

        return ResultResponse<PagedResponseRecord<MenuRecord>>.Success(response);
    }

    public async Task<ResultResponse<MenuRecord>> GetMenuById<T>(Guid id)
    {
        var menu = await _menuRepository.GetByIdAsync(id);

        if (menu == null)
        {
            return ResultResponse<MenuRecord>.Failure($"No se encontro el menu con el id: {id}", 404);
        }

        MenuRecord menuRecord = new MenuRecord(menu);

        return ResultResponse<MenuRecord>.Success(menuRecord);
    }

    public async Task<ResultResponse<MenuRecord>> CreateMenu (MenuCreateRecord menuCreateRecord)
    {

        var menu = Menu.CreateFromRecord(menuCreateRecord);

        await _menuRepository.AddAsync(menu);
        await _menuRepository.SaveAsync();

        return ResultResponse<MenuRecord>.Success(new MenuRecord(menu), 201);
    }

    public async Task<ResultResponse<MenuRecord>> UpdateMenu(MenuUpdateRecord menuUpdateRecord)
    {
        var menu = await _menuRepository.GetByIdAsync(menuUpdateRecord.Id);

        if(menu == null)
        {
            return ResultResponse<MenuRecord>.Failure($"No se encontro el menu con el id: {menuUpdateRecord.Id}", 404);
        }

        menu.UpdateFromRecord(menuUpdateRecord);

        await _menuRepository.UpdateAsync(menu);

        return ResultResponse<MenuRecord>.Success(new MenuRecord(menu), 200);
    }

    public async Task<ResultResponse<bool>> DeleteMenu(Guid id)
    {
        var menu = await _menuRepository.GetByIdAsync(id);

        if(menu == null)
        {
            return ResultResponse<bool>.Failure($"No se encontro el menu con el id: {id}", 404);
        }

        menu.eliminar();

        await _menuRepository.UpdateAsync(menu);

        return ResultResponse<bool>.Success(true, 200);
    }
}