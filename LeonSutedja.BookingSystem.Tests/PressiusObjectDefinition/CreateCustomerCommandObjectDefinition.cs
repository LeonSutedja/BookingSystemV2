using System.Collections.Generic;
using LeonSutedja.BookingSystem.Services.Commands;
using Pressius;

namespace LeonSutedja.BookingSystem.Tests
{
    public class ValidName : DefaultParameterDefinition
    {
        public override List<object> InputCatalogues =>
            new List<object> {
                "John",
                "Doe the 1st",
                "Mary 'ust",
                "Jane",
                "~!@#$%&*()_+=-`\\][{}|;:,./?><'\"" };

        public override ParameterTypeDefinition TypeName => new ParameterTypeDefinition("ValidName");
    }

    public class ValidMobileNo : DefaultParameterDefinition
    {
        public override List<object> InputCatalogues =>
            new List<object> {
                "0410555555",
                "613410555555"
            };

        public override ParameterTypeDefinition TypeName => new ParameterTypeDefinition("ValidMobileNo");
    }

    public class ValidEmail : DefaultParameterDefinition
    {
        public override List<object> InputCatalogues =>
            new List<object> {
                "Abcdefg@something.com",
                "Xyz@blah.com"
            };

        public override ParameterTypeDefinition TypeName => new ParameterTypeDefinition("ValidEmail");
    }

    public class CreateCustomerCommandObjectDefinition : PropertiesObjectDefinition<CreateCustomerCommand>
    {
        public override Dictionary<string, string> MatcherDictionary =>
            new Dictionary<string, string>
            {
                { "FirstName", "ValidName" },
                { "LastName", "ValidName" },
                { "Email", "ValidEmail" },
                { "MobileNo", "ValidMobileNo" },
            };
    }
}