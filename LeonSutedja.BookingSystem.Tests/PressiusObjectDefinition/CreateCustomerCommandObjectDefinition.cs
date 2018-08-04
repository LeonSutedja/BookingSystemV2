using System.Collections.Generic;
using LeonSutedja.BookingSystem.Services.Commands;
using Pressius;

namespace LeonSutedja.BookingSystem.Tests
{
    public class ValidNameParameter : DefaultParameterDefinition
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

    public class CreateCustomerCommandObjectDefinition : PropertiesObjectDefinition<CreateCustomerCommand>
    {
        public override Dictionary<string, string> MatcherDictionary =>
            new Dictionary<string, string>
            {
                { "FirstName", "ValidName" },
                { "LastName", "ValidName" },
                { "DateOfBirth", "DateTime" },
                { "Email", "Email" },
                { "MobileNo", "String" },
            };
    }
}