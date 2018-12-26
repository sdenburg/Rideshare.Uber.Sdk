using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class UberError
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
    }
}