using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Repositories.Interface;

namespace AnimalProtecction.Generated.Repositories.Repository
{
    public class ArchivoRepository : GenericRepository<Archivo>, IArchivoRepository
    {
        public ArchivoRepository(AnimalprotectionContext context) : base(context)
        {
        }
    }
}
