using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace LeonSutedja.BookingSystem
{
    [DependsOn(typeof(AbpWebApiModule), typeof(BookingSystemApplicationModule))]
    public class BookingSystemWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(BookingSystemApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
