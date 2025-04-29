using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class RedesSocialesRepository : GenericRepository<RedesSociales>, IRedesSocialesRepository
{
    public RedesSocialesRepository(AnimalprotectionContext context) : base(context)
    {
    }
}