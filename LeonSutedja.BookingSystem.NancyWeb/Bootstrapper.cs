using Nancy;
using Nancy.TinyIoc;
using LeonSutedja.BookingSystem.Shared.Handler.Create;
using LeonSutedja.BookingSystem.Shared.Handler.Delete;
using LeonSutedja.BookingSystem.Shared.Handler.Update;
using LeonSutedja.BookingSystem.Shared.Handler.Validation;

namespace LeonSutedja.BookingSystem.NancyWeb
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            //container.Register(typeof(IBusinessRule<,>), typeof(AzureRepository<>)).AsMultiInstance();
        }
    }
}