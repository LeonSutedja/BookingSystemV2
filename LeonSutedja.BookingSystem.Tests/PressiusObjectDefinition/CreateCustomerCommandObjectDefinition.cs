using System.Collections.Generic;
using LeonSutedja.BookingSystem.Services.Commands;
using Pressius;

namespace LeonSutedja.BookingSystem.Tests
{
    public class CreateCustomerCommandObjectDefinition : PropertiesObjectDefinition<CreateCustomerCommand>
    {
        public override Dictionary<string, string> MatcherDictionary =>
            new Dictionary<string, string>
            {
                { "FirstName", "String" },
                { "LastName", "String" },
                { "DateOfBirth", "DateTime" },
                { "Email", "Email" },
                { "MobileNo", "String" },
            };
    }
}