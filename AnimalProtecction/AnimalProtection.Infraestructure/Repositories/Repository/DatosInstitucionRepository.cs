using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class DatosInstitucionRepository : GenericRepository<DatosInstitucion>, IDatosInstitucionRepository
{
    public DatosInstitucionRepository(AnimalprotectionContext context) : base(context)
    {
    }
}