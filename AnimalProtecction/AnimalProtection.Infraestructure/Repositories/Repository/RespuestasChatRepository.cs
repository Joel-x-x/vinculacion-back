using AnimalProtecction.Domain.Entities;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class RespuestasChatRepository : GenericRepository<RespuestasChat>, IRespuestasChatRepository
{
    public RespuestasChatRepository(AnimalprotectionContext context) : base(context)
    {
    }
}