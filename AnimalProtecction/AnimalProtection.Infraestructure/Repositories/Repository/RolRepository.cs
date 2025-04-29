using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;
using AnimalProtecction.GenericRepository;

namespace AnimalProtecction.Generated.Repositories.Interface;

public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    public RolRepository(AnimalprotectionContext context) : base(context)
    {
    }
}