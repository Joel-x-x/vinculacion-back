using AnimalProtecction.Domain.Entities;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class PreguntasChatRepository : GenericRepository<PreguntasChat>, IPreguntasChatRepository
{
    public PreguntasChatRepository(AnimalprotectionContext context) : base(context)
    {
    }
}