using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class TiposCampanaRepository : GenericRepository<Tiposcampana>, ITiposCampanaRepository
{
    public TiposCampanaRepository(AnimalprotectionContext context) : base(context)
    {
    }
}