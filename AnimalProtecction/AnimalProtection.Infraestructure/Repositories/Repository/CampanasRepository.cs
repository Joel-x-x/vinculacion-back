using AnimalProtecction.Domain.Entities;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class CampanasRepository : GenericRepository<Campanas>, ICampanasRepository
{
    public CampanasRepository(AnimalprotectionContext context) : base(context)
    {
    }
}