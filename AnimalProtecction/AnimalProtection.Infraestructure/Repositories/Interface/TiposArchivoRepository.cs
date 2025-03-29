using AnimalProtecction.Generated;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Repositories.Interface
{
    public class TiposArchivoRepository : GenericRepository<Tiposarchivo>, ITiposArchivoRepository
    {
        public TiposArchivoRepository(AnimalprotectionContext context) : base(context)
        {
        }
    }
}
