using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class Promotion
    {
        [JsonProperty(PropertyName = "display_text")]
        public string DisplayText { get; set; }

        [JsonProperty(PropertyName = "localized_value")]
        public string LocalizedValue { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}