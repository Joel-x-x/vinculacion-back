using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Repositories.Interface;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class EspecyRepository : GenericRepository<Especy>, IEspecyRepository
{
    public EspecyRepository(AnimalprotectionContext context) : base(context)
    {
    }
}