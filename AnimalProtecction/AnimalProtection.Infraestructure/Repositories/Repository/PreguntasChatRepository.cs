using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class PreguntasChatRepository : GenericRepository<Preguntaschat>, IPreguntasChatRepository
{
    public PreguntasChatRepository(AnimalprotectionContext context) : base(context)
    {
    }
}