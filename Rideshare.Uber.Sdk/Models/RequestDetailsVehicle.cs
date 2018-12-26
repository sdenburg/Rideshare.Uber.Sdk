using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class RequestDetailsVehicle
    {
        [JsonProperty(PropertyName = "make")]
        public string Make { get; set; }

        [JsonProperty(PropertyName = "model")]
        public string Model { get; set; }

        [JsonProperty(PropertyName = "license_plate")]
        public string LicensePlate { get; set; }

        [JsonProperty(PropertyName = "picture_url")]
        public string PictureUrl { get; set; }
    }
}