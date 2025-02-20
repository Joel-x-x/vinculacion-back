using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class PreguntasFrecuentesRepository : GenericRepository<PreguntasFrecuentes>, IPreguntasFrecuentesRepository
{
    public PreguntasFrecuentesRepository(AnimalprotectionContext context) : base(context)
    {
    }
}