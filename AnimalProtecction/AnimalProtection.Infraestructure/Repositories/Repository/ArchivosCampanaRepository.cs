using AnimalProtecction.Domain.Entities;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class ArchivosCampanaRepository : GenericRepository<ArchivosCampana>, IArchivosCampanaRepository
{
    public ArchivosCampanaRepository(AnimalprotectionContext context) : base(context)
    {
    }
}