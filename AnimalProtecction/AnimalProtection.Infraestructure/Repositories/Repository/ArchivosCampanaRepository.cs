using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class ArchivosCampanaRepository : GenericRepository<Archivoscampana>, IArchivosCampanaRepository
{
    public ArchivosCampanaRepository(AnimalprotectionContext context) : base(context)
    {
    }
}