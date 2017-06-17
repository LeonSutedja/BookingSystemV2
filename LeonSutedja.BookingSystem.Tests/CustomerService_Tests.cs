using System;
using Shouldly;
using Xunit;

namespace LeonSutedja.BookingSystem.Tests
{
    public class CustomerService_Tests : BookingSystemTestBase
    {
        //private readonly ITaskAppService _taskAppService;

        public CustomerService_Tests()
        {
            //Creating the class which is tested (SUT - Software Under Test)
            //_taskAppService = LocalIocManager.Resolve<ITaskAppService>();
        }

        [Fact]
        public void Should_Create_New_Customer()
        {
            //Prepare for test
            var c = new Customer.Customer("James", "Powell", new DateTime(1977, 11, 11));
            c.Id.ShouldBeGreaterThan(0);
        }
    }
}