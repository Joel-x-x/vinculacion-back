using AnimalProtecction.Generated;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Domain.Entities;
using AnimalProtection.Repositories.Interface;

namespace AnimalProtection.Repositories.Repository;

public class EstadostramiteRepository(AnimalprotectionContext context)
    : GenericRepository<Estadostramite>(context), IEstadostramiteRepository;