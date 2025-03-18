using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Repositories.Interface;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class GeneroRepository : GenericRepository<Genero>, IGeneroRepository
{
    public GeneroRepository(AnimalprotectionContext context) : base(context)
    {
    }
}