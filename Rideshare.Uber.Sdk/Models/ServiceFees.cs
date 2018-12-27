using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class ServiceFees
    {
        /// <summary>
        /// The service fee name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The service fee amount
        /// </summary>
        [JsonProperty(PropertyName = "fee")]
        public string Fee { get; set; }
    }
}