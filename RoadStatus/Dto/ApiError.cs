using System;
using Newtonsoft.Json;

namespace RoadStatus.Dto
{
    public class ApiError
    {
        [JsonProperty("timestampUtc")]
        public DateTime TimestampUtc { get; set; }

        [JsonProperty("exceptionType")]
        public string ExceptionType { get; set; }

        [JsonProperty("httpStatusCode")]
        public int HttpStatusCode { get; set; }

        [JsonProperty("httpStatus")]
        public string HttpStatus { get; set; }

        [JsonProperty("relativeUrl")]
        public string RelativeUrl { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
