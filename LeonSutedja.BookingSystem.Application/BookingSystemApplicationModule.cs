using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using LeonSutedja.BookingSystem.Shared.Handler.Create;
using LeonSutedja.BookingSystem.Shared.Handler.Delete;
using LeonSutedja.BookingSystem.Shared.Handler.Update;
using LeonSutedja.BookingSystem.Shared.Handler.Validation;

namespace LeonSutedja.BookingSystem
{
    [DependsOn(typeof(BookingSystemCoreModule), typeof(AbpAutoMapperModule))]
    public class BookingSystemApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Kernel.Resolver
                .AddSubResolver(new CollectionResolver(IocManager.IocContainer.Kernel, true));

            IocManager.IocContainer.Register(
                Classes.FromThisAssembly()
                .BasedOn(typeof(IBusinessRule<,>))
                .WithService.AllInterfaces()
                .LifestyleTransient()
                .AllowMultipleMatches());

            IocManager.IocContainer.Register(
                Component.For(typeof(IDeleteHandler<,>))
                .ImplementedBy(typeof(GenericDeleteHandler<,>))
                .LifestyleTransient());

            IocManager.IocContainer.Register(Component.For(typeof(IDeleteHandlerFactory))
                .ImplementedBy(typeof(DeleteHandlerFactory))
                .LifestyleTransient());

            IocManager.IocContainer.Register(
                Component.For(typeof(ICreateHandler<,>))
                .ImplementedBy(typeof(CreateDecoratorEventLogger<,>)),
                Component.For(typeof(ICreateHandler<,>))
                .ImplementedBy(typeof(GenericCreateHandler<,>))
                .LifestyleTransient());

            IocManager.IocContainer.Register(Component.For(typeof(ICreateHandlerFactory))
                .ImplementedBy(typeof(CreateHandlerFactory))
                .LifestyleTransient());

            IocManager.IocContainer.Register(
                Classes.FromThisAssembly()
                .BasedOn(typeof(ICreateCommandMapper<,>))
                .WithService.AllInterfaces()
                .LifestyleTransient()
                .AllowMultipleMatches());

            IocManager.IocContainer.Register(Component.For(typeof(ICreateCommandMapperFactory))
                .ImplementedBy(typeof(CreateCommandMapperFactory))
                .LifestyleTransient());

            IocManager.IocContainer.Register(
               Component.For(typeof(IUpdateHandler<,>))
               .ImplementedBy(typeof(UpdateDecoratorLogger<,>)),
               Component.For(typeof(IUpdateHandler<,>))
               .ImplementedBy(typeof(GenericUpdateHandler<,>))
               .LifestyleTransient());

            IocManager.IocContainer.Register(Component.For(typeof(IUpdateHandlerFactory))
                .ImplementedBy(typeof(UpdateHandlerFactory))
                .LifestyleTransient());

            IocManager.IocContainer.Register(
                Classes.FromThisAssembly()
                .BasedOn(typeof(IUpdateCommandHandler<,>))
                .WithService.AllInterfaces()
                .LifestyleTransient()
                .AllowMultipleMatches());

            IocManager.IocContainer.Register(
                Component.For(typeof(IUpdateCommandExecutorFactory))
                .ImplementedBy(typeof(UpdateCommandExecutorFactory))
                .LifestyleTransient());
        }
    }
}
