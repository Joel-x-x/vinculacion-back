using AnimalProtecction.Generated;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Repositories.Repository;

public class TramiteRepository : GenericRepository<Tramite>, ITramiteRepository
{
    public TramiteRepository(AnimalprotectionContext context) : base(context)
    {
    }
}