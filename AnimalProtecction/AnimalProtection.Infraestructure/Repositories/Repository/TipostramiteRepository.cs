using AnimalProtecction.Generated;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Repositories.Interface;

namespace AnimalProtection.Repositories.Repository;

public class TipostramiteRepository(AnimalprotectionContext context)
    : GenericRepository<Tipostramite>(context), ITipostramiteRepository;