using AnimalProtecction.Generated;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Repositories.Repository;

public class TipostramiteRepository : GenericRepository<Tipostramite>, ITipostramiteRepository
{
    public TipostramiteRepository(AnimalprotectionContext context) : base(context)
    {
    }
}