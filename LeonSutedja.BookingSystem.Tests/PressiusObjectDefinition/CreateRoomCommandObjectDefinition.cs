using System.Collections.Generic;
using LeonSutedja.BookingSystem.Services.Commands;
using Pressius;

namespace LeonSutedja.BookingSystem.Tests
{
    public class ValidShortRoomName : DefaultParameterDefinition
    {
        public override List<object> InputCatalogues =>
            new List<object> {
                "XVR-01",
                "~!@#$%&*()_+=-`\\][",
                "{}|;:,./?><'\""
            };

        public override ParameterTypeDefinition TypeName => new ParameterTypeDefinition("ValidShortRoomName");
    }

    public class ValidRoomName : DefaultParameterDefinition
    {
        public override List<object> InputCatalogues =>
            new List<object> {
                "Xavier-01",
                "~!@#$%&*()_+=-`\\][{}|;:,./?><'\"" };

        public override ParameterTypeDefinition TypeName => new ParameterTypeDefinition("ValidRoomName");
    }

    public class ValidLocation : DefaultParameterDefinition
    {
        public override List<object> InputCatalogues =>
            new List<object> {
                "Mens Building, 10 Latrobe Street, VIC 3000, Melbourne, Australia",
                "~!@#$%&*()_+=-`\\][{}|;:,./?><'\"" };

        public override ParameterTypeDefinition TypeName => new ParameterTypeDefinition("ValidLocation");
    }

    public class CreateRoomCommandObjectDefinition : PropertiesObjectDefinition<CreateRoomCommand>
    {
        public override Dictionary<string, string> MatcherDictionary =>
            new Dictionary<string, string>
            {
                { "ShortName", "ValidShortRoomName" },
                { "Name", "ValidRoomName" },
                { "Location", "ValidLocation" }
            };
    }
}