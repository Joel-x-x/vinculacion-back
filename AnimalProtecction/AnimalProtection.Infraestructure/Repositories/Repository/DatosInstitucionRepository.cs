using AnimalProtecction.Domain.Entities;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class DatosInstitucionRepository : GenericRepository<DatosInstitucion>, IDatosInstitucionRepository
{
    public DatosInstitucionRepository(AnimalprotectionContext context) : base(context)
    {
    }
}