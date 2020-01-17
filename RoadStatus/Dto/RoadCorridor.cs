using Newtonsoft.Json;

namespace RoadStatus.Dto
{
    public class RoadCorridor
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("statusSeverity")]
        public string StatusSeverity { get; set; }

        [JsonProperty("statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; }

        [JsonProperty("bounds")]
        public  string Bounds { get; set; }

        [JsonProperty("envelope")]
        public string Envelope { get; set; }

        [JsonProperty("url")]
        public string ResourceUrl  { get; set; }

    }
}
