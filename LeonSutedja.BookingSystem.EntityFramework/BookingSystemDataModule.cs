using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using LeonSutedja.BookingSystem.EntityFramework;

namespace LeonSutedja.BookingSystem
{
    [DependsOn(
        typeof(AbpEntityFrameworkModule), 
        typeof(BookingSystemCoreModule))]
    public class BookingSystemDataModule : AbpModule
    {
        //public override void PreInitialize()
        //{
        //    Configuration.DefaultNameOrConnectionString = "Default";
        //}

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //Database.SetInitializer<BookingSystemDbContext>(null);
        }
    }
}
