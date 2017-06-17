using Abp.Modules;

namespace LeonSutedja.BookingSystem.Tests
{
    [DependsOn(typeof(BookingSystemDataModule), typeof(BookingSystemApplicationModule))]
    public class BookingSystemTestModule : AbpModule
    {
    }
}