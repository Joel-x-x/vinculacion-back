using AnimalProtecction.Domain.Entities;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class CooperantesRepository : GenericRepository<Cooperantes>,ICooperantesRepository
{
    public CooperantesRepository(AnimalprotectionContext context) : base(context)
    {
    }
}