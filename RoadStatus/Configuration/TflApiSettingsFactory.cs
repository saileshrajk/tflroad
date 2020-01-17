using Microsoft.Extensions.Configuration;

namespace RoadStatus.Configuration
{
    static class TflApiSettingsFactory
    {
        public static TflApiSettings GetTflApiSettings(IConfiguration configuration)
        {
            var tflSettingsSection = configuration.GetSection("TflApiSettings");

            if (tflSettingsSection == null)
            {
                throw new InvalidApiConfigurationException();
            }

            return new TflApiSettings
            {
                ApplicationId = tflSettingsSection["ApplicationId"],
                ApplicationKey = tflSettingsSection["ApplicationKey"],
                BaseUrl = tflSettingsSection["BaseUrl"],
                RoadResource = tflSettingsSection["RoadResource"]
            };
        }
    }
}
