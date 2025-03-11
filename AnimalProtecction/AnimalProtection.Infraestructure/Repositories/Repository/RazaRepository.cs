using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Repositories.Interface;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class RazaRepository : GenericRepository<Raza>, IRazaRepository
{
    public RazaRepository(AnimalprotectionContext context) : base(context)
    {
    }
}