using AnimalProtecction.Generated;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Repositories.Repository;

public class PreguntasFrecuenteRepository : GenericRepository<Preguntasfrecuente>, IPreguntasFrecuenteRepository
{
    public PreguntasFrecuenteRepository(AnimalprotectionContext context) : base(context)
    {
    }
}