using Nancy;
using Nancy.TinyIoc;

namespace LeonSutedja.BookingSystem.NancyWeb
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
        }
    }
}