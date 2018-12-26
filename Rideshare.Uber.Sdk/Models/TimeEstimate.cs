using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class TimeEstimate
    {
        [JsonProperty(PropertyName = "product_id")]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "estimate")]
        public int Estimate { get; set; }
    }
}