using AnimalProtecction.Generated;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Repositories.Repository;

public class CampanaRepository : GenericRepository<Campana>, ICampanaRepository
{
    public CampanaRepository(AnimalprotectionContext context) : base(context)
    {
    }
} 