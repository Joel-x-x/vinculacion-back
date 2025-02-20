using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class RespuestasChatRepository : GenericRepository<Respuestaschat>, IRespuestasChatRepository
{
    public RespuestasChatRepository(AnimalprotectionContext context) : base(context)
    {
    }
}