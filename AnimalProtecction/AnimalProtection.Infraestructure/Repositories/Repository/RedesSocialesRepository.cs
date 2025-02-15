using AnimalProtecction.Domain.Entities;
using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.GenericRepository;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class RedesSocialesRepository : GenericRepository<RedesSociales>, IRedesSocialesRepository
{
    public RedesSocialesRepository(AnimalprotectionContext context) : base(context)
    {
    }
}