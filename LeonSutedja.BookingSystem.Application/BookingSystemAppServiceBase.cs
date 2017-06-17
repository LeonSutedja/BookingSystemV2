using Abp.Application.Services;

namespace LeonSutedja.BookingSystem
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class BookingSystemAppServiceBase : ApplicationService
    {
        protected BookingSystemAppServiceBase()
        {
            LocalizationSourceName = BookingSystemConsts.LocalizationSourceName;
        }
    }
}