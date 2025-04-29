// using AnimalProtection.Application.Validator;
// using Autofac;
// using FluentValidation;
//
// namespace AnimalProtection.Api.Configuration;
//
// public class ValidationModule: Module
// {
//     protected override void Load(ContainerBuilder builder)
//     {
//         // Registrar todos los validadores del ensamblado
//         // builder.RegisterAssemblyTypes(typeof(LoginValidator).Assembly)
//         //     .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
//         //     .AsImplementedInterfaces()
//         //     .InstancePerLifetimeScope();
//
//         // Registrar IValidatorFactory
//         // builder.RegisterType<ServiceProviderValidatorFactory>()
//         //     .As<IValidatorFactory>()
//         //     .InstancePerLifetimeScope();
//     }
// }