using Abp.Web.Mvc.Views;

namespace LeonSutedja.BookingSystem.Web.Views
{
    public abstract class BookingSystemWebViewPageBase : BookingSystemWebViewPageBase<dynamic>
    {

    }

    public abstract class BookingSystemWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected BookingSystemWebViewPageBase()
        {
            LocalizationSourceName = BookingSystemConsts.LocalizationSourceName;
        }
    }
}