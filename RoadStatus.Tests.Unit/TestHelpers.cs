using System.IO;
using RoadStatus.Configuration;

namespace RoadStatus.Tests.Unit
{
    public static class TestHelpers
    {
        public static string GetValidJson()
        {
            var validJson = File.ReadAllText("validResponse.json");
            return validJson;
        }

        public static string GetNotFoundJson()
        {
            var notfoundJson = File.ReadAllText("notFoundResponse.json");
            return notfoundJson;
        }

        public static TflApiSettings GetFakeSettings()
        {
           return new TflApiSettings
            {
                ApplicationKey = "1234", 
                ApplicationId = "1234",
                BaseUrl = "http://fakeuri.com",
                RoadResource = "/road"
            };
        }
    }
}
