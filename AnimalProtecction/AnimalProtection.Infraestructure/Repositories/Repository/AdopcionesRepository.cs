using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class AdopcionesRepository : GenericRepository<Adopcione>, IAdopcionesRepository
{
    public AdopcionesRepository(AnimalprotectionContext context) : base(context)
    {
    }
}