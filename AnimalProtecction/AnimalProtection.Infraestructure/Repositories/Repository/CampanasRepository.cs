using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class CampanasRepository : GenericRepository<Campanas>, ICampanasRepository
{
    public CampanasRepository(AnimalprotectionContext context) : base(context)
    {
    }
}