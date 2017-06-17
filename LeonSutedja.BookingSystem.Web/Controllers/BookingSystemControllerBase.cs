using Abp.Web.Mvc.Controllers;

namespace LeonSutedja.BookingSystem.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class BookingSystemControllerBase : AbpController
    {
        protected BookingSystemControllerBase()
        {
            LocalizationSourceName = BookingSystemConsts.LocalizationSourceName;
        }
    }
}