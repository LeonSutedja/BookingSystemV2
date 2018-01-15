using LeonSutedja.BookingSystem.Services;
using LeonSutedja.BookingSystem.Services.Commands;
using LeonSutedja.BookingSystem.Shared;
using LeonSutedja.Pressius.TestEngine;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LeonSutedja.Pressius.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICudAppService _cudAppService;

        public HomeController(ICudAppService cudService)
        {
            _cudAppService = cudService;
        }

        public ActionResult Index()
        {
            var assemblyQualifiedName = typeof(CreateCustomerCommand).AssemblyQualifiedName;
            var inputGenerator = new InputGenerator();
            var inputLists = inputGenerator.CreateInputs(assemblyQualifiedName);

            var handlerResponses = new List<HandlerResponse>();
            foreach (var input in inputLists)
            {
                try
                {
                    var result = _cudAppService.CreateCustomer((CreateCustomerCommand)input);
                    handlerResponses.Add(result);
                    var resultMessage = result.Status + " " + result.Message;
                    Console.WriteLine(resultMessage.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return View();
        }

        public ActionResult About()
        {
            

            //var result = _cudAppService.CreateCustomer(command);
            //result.Id.ShouldBeGreaterThan(0);
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}