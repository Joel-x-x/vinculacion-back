using AnimalProtection.Application.Commands.Token;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Application.Querys.Service;
using AnimalProtection.Repositories.Repository;
using Autofac;
using Module = Autofac.Module;

namespace AnimalProtection.Api.Configuration;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UsuarioQueryService>()
            .As<IUsuarioQueryService>();

        builder.RegisterType<TokenService>()
            .As<ITokenService>();  
            
        builder.RegisterType<TramiteQueryService>()
             .As<ITramiteQueryService>();

        builder.RegisterType<TipostramiteQueryService>()
             .As<ITipostramiteQueryService>();

        builder.RegisterType<EstadostramiteQueryService>()
             .As<IEstadostramiteQueryService>();
  
        builder.RegisterType<PreguntasFrecuenteQueryService>()
             .As<IPreguntasFrecuenteQueryService>();

        builder.RegisterType<DatosInstitucionService>()
            .As<IDatosInstitucionService>();
            
        builder.RegisterType<CooperantesQueryService>()
            .As<ICooperantesQueryService>();

        builder.RegisterType<GeneroQueryService>()
            .As<IGeneroQueryService>();
            
        builder.RegisterType<EspecyQueryService>()
            .As<IEspecyQueryService>();
            
        builder.RegisterType<RazaQueryService>()
            .As<IRazaQueryService>();
            
        builder.RegisterType<MascotaQueryService>()
            .As<IMascotaQueryService>();
            
        builder.RegisterType<RolService>()
            .As<IRolService>();

        builder.RegisterType<AdopcionesQueryService>()
            .As<IAdopcionesQueryService>();
        
        builder.RegisterType<MenuQueryService>()
            .As<IMenuQueryService>();

        builder.RegisterType<ArchivoQueryService>()
            .As<IArchivoQueryService>();
            
        builder.RegisterType<TiposArchivoQueryService>()
            .As<ITiposArchivoQueryService>();
        
        builder.RegisterType<CooperantesQueryService>()
            .As<ICooperantesQueryService>();

        builder.RegisterType<CampanaQueryService>()
            .As<ICampanaQueryService>();

        builder.RegisterType<ArchivoCampanaQueryService>()
            .As<IArchivoCampanaQueryService>();
    }
}