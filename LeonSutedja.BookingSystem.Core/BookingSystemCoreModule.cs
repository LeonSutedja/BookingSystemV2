using System.Reflection;
using Abp.Modules;

namespace LeonSutedja.BookingSystem
{
    public class BookingSystemCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
