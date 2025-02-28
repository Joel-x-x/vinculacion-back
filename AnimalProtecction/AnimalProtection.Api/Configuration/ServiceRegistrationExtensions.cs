using AnimalProtection.Application.Commands.Token;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Application.Querys.Service;
using Autofac;
using Module = Autofac.Module;

namespace AnimalProtection.Api.Configuration;

public  class ApplicationModule: Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UsuarioQueryService>()
            .As<IUsuarioQueryService>();

        builder.RegisterType<TokenService>()
            .As<ITokenService>();
<<<<<<< HEAD
        
       builder.RegisterType<TramiteQueryService>()
            .As<ITramiteQueryService>();

        builder.RegisterType<DatosInstitucionService>()
            .As<IDatosInstitucionService>();
=======

        builder.RegisterType<TramiteQueryService>()
                .As<ITramiteQueryService>();

        builder.RegisterType<CooperantesQueryService>()
            .As<ICooperantesQueryService>();
>>>>>>> 2c532b162d487455daae51f0e3fd093519d91b16
    }
}