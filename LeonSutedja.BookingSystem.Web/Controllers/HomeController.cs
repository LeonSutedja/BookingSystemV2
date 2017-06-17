using System.Web.Mvc;

namespace LeonSutedja.BookingSystem.Web.Controllers
{
    public class HomeController : BookingSystemControllerBase
    {
        public ActionResult Index()
        { 
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}