using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class RequestDetailsLocation
    {
        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }
    }
}