using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Repositories.Interface;

namespace AnimalProtecction.Generated.Repositories.Repository;

public class MascotaRepository : GenericRepository<Mascota>, IMascotaRepository
{
    public MascotaRepository(AnimalprotectionContext context) : base(context)
    {
    }
}