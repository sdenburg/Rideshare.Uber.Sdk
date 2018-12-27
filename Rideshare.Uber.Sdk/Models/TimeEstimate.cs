using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class TimeEstimate
    {
        /// <summary>
        /// Unique identifier representing a specific product for a given latitude & longitude.
        /// For example, uberX in San Francisco will have a different product_id than uberX in Los Angeles.
        /// </summary>
        [JsonProperty(PropertyName = "product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// Localized display name of product.
        /// </summary>
        [JsonProperty(PropertyName = "localized_display_name")]
        public string LocalizedDisplayName { get; set; }

        /// <summary>
        /// Display name of product.
        /// </summary>
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// ETA for the product (in seconds). Always show estimate in minutes.
        /// </summary>
        [JsonProperty(PropertyName = "estimate")]
        public int Estimate { get; set; }
    }
}