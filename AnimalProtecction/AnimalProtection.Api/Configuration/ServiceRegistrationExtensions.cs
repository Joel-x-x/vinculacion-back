using System.Reflection;
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
    }
}