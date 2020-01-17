using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RoadStatus.Configuration;
using RoadStatus.Dto;
using RoadStatus.Entity;

namespace RoadStatus.Repository
{
    public class TflRoadStatusRepository : IRoadStatusRepository
    {
        private const string AppIdName = "app_id";
        private const string AppKeyName = "app_key";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TflApiSettings _settings;

        public TflRoadStatusRepository(IHttpClientFactory httpClientFactory, TflApiSettings settings)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<IRoad> GetRoadStatus(string roadName)
        {
            var client = _httpClientFactory
                .CreateClient(HttpClientNames.TflHttpClientName);

            var httpResponseMessage = await client.GetAsync(BuildRequestUri(roadName));

            return await ParseRoadStatusResponse(httpResponseMessage, roadName);
        }

    
        private string BuildRequestUri(string roadName)
        {
            return $"{_settings.RoadResource}/{roadName}?" +
                   $"{AppIdName}={_settings.ApplicationId}&" +
                   $"{AppKeyName}={_settings.ApplicationKey}";
        }

        private async Task<IRoad> ParseRoadStatusResponse(HttpResponseMessage httpResponseMessage, string roadName)
        {
            var content = await httpResponseMessage.Content
                .ReadAsStringAsync();

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                return GetRoadForSuccessfulResponse(content, roadName);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                return new InvalidRoad(roadName);
            }

            return new UnknownRoad(roadName);
        }

        private IRoad GetRoadForSuccessfulResponse(string jsonContent, string roadName)
        {
            var roadCorridors = JsonConvert.DeserializeObject<List<RoadCorridor>>(jsonContent);

            if (roadCorridors == null || !roadCorridors.Any())
            {
                return new UnknownRoad(roadName);
            }

            var roadCorridor = roadCorridors.First();

            return new ValidRoad(
                roadCorridor.DisplayName,
                roadCorridor.StatusSeverity,
                roadCorridor.StatusSeverityDescription);
        }
    }
}
