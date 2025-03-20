using AnimalProtecction.Generated;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Repositories.Interface;

namespace AnimalProtection.Repositories.Repository;

public class TipostramiteRepository : GenericRepository<Tipostramite>, ITipostramiteRepository
{
    public TipostramiteRepository(AnimalprotectionContext context) : base(context)
    {
    }
}