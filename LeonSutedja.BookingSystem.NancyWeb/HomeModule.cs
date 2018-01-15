using Nancy;

namespace LeonSutedja.BookingSystem.NancyWeb
{

    public interface ITestInterface
    {
        string GetSomeString();
    }

    public class TestInterfaceClaass : ITestInterface
    {
        public string GetSomeString()
        {
            return "Welcome to Nancy!";
        }
    }


    public class HomeModule : NancyModule
    {
        public HomeModule(ITestInterface testInterface)
        {
            Get["/"] = _ =>
            {
                dynamic viewBag = new DynamicDictionary();
                viewBag.WelcomeMessage = testInterface.GetSomeString();
                return View["home", viewBag];
            };
        }
    }
}