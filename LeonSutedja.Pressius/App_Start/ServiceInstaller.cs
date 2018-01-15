using Abp.Domain.Repositories;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LeonSutedja.BookingSystem.Services;
using LeonSutedja.BookingSystem.Shared.Handler.Create;
using LeonSutedja.BookingSystem.Shared.Handler.Delete;
using LeonSutedja.BookingSystem.Shared.Handler.Update;
using LeonSutedja.BookingSystem.Shared.Handler.Validation;

namespace LeonSutedja.Pressius.App_Start
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
               Classes.FromAssemblyInThisApplication()
               .BasedOn(typeof(IRepository<>))
               .WithService.AllInterfaces()
               .LifestyleTransient()
               .AllowMultipleMatches());
            
            container.Register(
                Classes.FromAssemblyInThisApplication()
                .BasedOn(typeof(IBusinessRule<,>))
                .WithService.AllInterfaces()
                .LifestyleTransient()
                .AllowMultipleMatches());

            container.Register(
                Component.For(typeof(IDeleteHandler<,>))
                .ImplementedBy(typeof(GenericDeleteHandler<,>))
                .LifestyleTransient());

            container.Register(Component.For(typeof(IDeleteHandlerFactory))
                .ImplementedBy(typeof(DeleteHandlerFactory))
                .LifestyleTransient());

            container.Register(
                Component.For(typeof(ICreateHandler<,>))
                .ImplementedBy(typeof(CreateDecoratorEventLogger<,>)),
                Component.For(typeof(ICreateHandler<,>))
                .ImplementedBy(typeof(GenericCreateHandler<,>))
                .LifestyleTransient());

            container.Register(Component.For(typeof(ICreateHandlerFactory))
                .ImplementedBy(typeof(CreateHandlerFactory))
                .LifestyleTransient());

            container.Register(
                Classes.FromThisAssembly()
                .BasedOn(typeof(ICreateCommandMapper<,>))
                .WithService.AllInterfaces()
                .LifestyleTransient()
                .AllowMultipleMatches());

            container.Register(Component.For(typeof(ICreateCommandMapperFactory))
                .ImplementedBy(typeof(CreateCommandMapperFactory))
                .LifestyleTransient());

            container.Register(
               Component.For(typeof(IUpdateHandler<,>))
               .ImplementedBy(typeof(UpdateDecoratorLogger<,>)),
               Component.For(typeof(IUpdateHandler<,>))
               .ImplementedBy(typeof(GenericUpdateHandler<,>))
               .LifestyleTransient());

            container.Register(Component.For(typeof(IUpdateHandlerFactory))
                .ImplementedBy(typeof(UpdateHandlerFactory))
                .LifestyleTransient());

            container.Register(
                Classes.FromThisAssembly()
                .BasedOn(typeof(IUpdateCommandHandler<,>))
                .WithService.AllInterfaces()
                .LifestyleTransient()
                .AllowMultipleMatches());

            container.Register(
                Component.For(typeof(IUpdateCommandExecutorFactory))
                .ImplementedBy(typeof(UpdateCommandExecutorFactory))
                .LifestyleTransient());

            container.Register(
                Component.For(typeof(ICudAppService))
                .ImplementedBy(typeof(CudAppService))
                .LifestyleTransient());
        }
    }
}