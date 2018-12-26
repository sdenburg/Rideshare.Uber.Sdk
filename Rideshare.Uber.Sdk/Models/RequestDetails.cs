using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class RequestDetails
    {
        [JsonProperty(PropertyName = "request_id")]
        public string RequestId { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "vehicle")]
        public RequestDetailsVehicle Vehicle { get; set; }

        [JsonProperty(PropertyName = "driver")]
        public RequestDetailsDriver Driver { get; set; }

        [JsonProperty(PropertyName = "location")]
        public RequestDetailsLocation Location { get; set; }

        [JsonProperty(PropertyName = "eta")]
        public int? ETA { get; set; }

        [JsonProperty(PropertyName = "surge_multiplier")]
        public float SurgeMultiplier { get; set; }
    }
}