using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface IMenuQueryService
{
    Task<ResultResponse<PagedResponseRecord<MenuRecord>>> GetAllMenus(int pageNumber, int pageSize);
    Task<ResultResponse<MenuRecord>> GetMenuById<T>(Guid id);
    Task<ResultResponse<MenuRecord>> CreateMenu(MenuCreateRecord menuCreateRecord);
    Task<ResultResponse<MenuRecord>> UpdateMenu(MenuUpdateRecord menuUpdateRecord);
    Task<ResultResponse<bool>> DeleteMenu(Guid id);
}