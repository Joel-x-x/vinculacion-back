using AnimalProtecction.Domain.Entities;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class TiposCampanaRepository : GenericRepository<TiposCampana>, ITiposCampanaRepository
{
    public TiposCampanaRepository(AnimalprotectionContext context) : base(context)
    {
    }
}