using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtecction.Generated.Repositories.Repository;
using AnimalProtecction.GenericRepository;
using AnimalProtection.Repositories.Repository;
using Autofac;

namespace AnimalProtection.Api.Configuration;

public class InfrastructureModule: Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(GenericRepository<>))
            .As(typeof(IGenericRepository<>))
            .InstancePerLifetimeScope();
        
        builder.RegisterType<UsuarioRepository>()
            .As<IUsuarioRepository>();
        
        builder.RegisterType<TramiteRepository>()
            .As<ITramiteRepository>();
<<<<<<< HEAD

        builder.RegisterType<DatosInstitucionRepository>()
            .As<IDatosInstitucionRepository>();
=======
        
        builder.RegisterType<CooperantesRepository>()
            .As<ICooperantesRepository>();
>>>>>>> 2c532b162d487455daae51f0e3fd093519d91b16
    }
}