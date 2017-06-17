using System;
using System.Data.Common;
using Abp.Dependency;
using Abp.TestBase;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using EntityFramework.DynamicFilters;
using LeonSutedja.BookingSystem.EntityFramework;

namespace LeonSutedja.BookingSystem.Tests
{
    public abstract class BookingSystemTestBase : AbpIntegratedTestBase<BookingSystemTestModule>
    {
        protected BookingSystemTestBase()
        {
            //Seed initial data
            UsingDbContext(context => new BookingSystemInitialDataBuilder().Build(context));
        }

        protected override void PreInitialize()
        {
            //Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            //Fake DbConnection using Effort!
            LocalIocManager.IocContainer.Register(
                Component.For<DbConnection>()
                    .UsingFactoryMethod(Effort.DbConnectionFactory.CreateTransient)
                    .LifestyleSingleton()
                );

            
            base.PreInitialize();
        }

        public void UsingDbContext(Action<BookingSystemDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<BookingSystemDbContext>())
            {
                context.DisableAllFilters();
                action(context);
                context.SaveChanges();
            }
        }

        public T UsingDbContext<T>(Func<BookingSystemDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<BookingSystemDbContext>())
            {
                context.DisableAllFilters();
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
