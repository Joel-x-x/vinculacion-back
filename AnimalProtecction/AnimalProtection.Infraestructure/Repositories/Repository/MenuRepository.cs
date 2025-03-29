using AnimalProtecction.Generated;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Repositories.Repository;

public class MenuRepository : GenericRepository<Menu>, IMenuRepository
{
    public MenuRepository(AnimalprotectionContext context) : base(context)
    {
    }
}