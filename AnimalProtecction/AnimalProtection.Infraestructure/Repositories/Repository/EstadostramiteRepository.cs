using AnimalProtecction.Generated;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Repositories.Interface;

namespace AnimalProtection.Repositories.Repository;

public class EstadostramiteRepository : GenericRepository<Estadostramite>, IEstadostramiteRepository
{
    public EstadostramiteRepository(AnimalprotectionContext context) : base(context)
    {
    }
}