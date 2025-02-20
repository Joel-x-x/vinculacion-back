using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class CooperantesRepository : GenericRepository<Cooperantes>,ICooperantesRepository
{
    public CooperantesRepository(AnimalprotectionContext context) : base(context)
    {
    }
}