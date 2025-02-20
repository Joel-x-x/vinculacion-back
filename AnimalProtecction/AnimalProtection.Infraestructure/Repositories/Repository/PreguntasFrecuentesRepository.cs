using AnimalProtecction.Domain.Entities;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class PreguntasFrecuentesRepository : GenericRepository<PreguntasFrecuentes>, IPreguntasFrecuentesRepository
{
    public PreguntasFrecuentesRepository(AnimalprotectionContext context) : base(context)
    {
    }
}