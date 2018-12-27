using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class PriceEstimate
    {
        /// <summary>
        /// Localized display name of product.
        /// </summary>
        [JsonProperty(PropertyName = "localized_display_name")]
        public string LocalizedDisplayName { get; set; }

        /// <summary>
        /// Expected activity distance (in miles).
        /// </summary>
        [JsonProperty(PropertyName = "distance")]
        public float Distance { get; set; }

        /// <summary>
        /// Display name of product.
        /// </summary>
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Unique identifier representing a specific product for a given latitude & longitude.
        /// For example, uberX in San Francisco will have a different product_id than uberX in Los Angeles.
        /// </summary>
        [JsonProperty(PropertyName = "product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// Upper bound of the estimated price.
        /// </summary>
        [JsonProperty(PropertyName = "high_estimate")]
        public double? HighEstimate { get; set; }

        /// <summary>
        /// Lower bound of the estimated price.
        /// </summary>
        [JsonProperty(PropertyName = "low_estimate")]
        public double? LowEstimate { get; set; }

        /// <summary>
        /// Expected activity duration (in seconds). Always show duration in minutes.
        /// </summary>
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }

        /// <summary>
        /// ISO 4217 currency code.
        /// </summary>
        [JsonProperty(PropertyName = "CurrencyCode")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Formatted string of estimate in local currency of the start location.
        /// Estimate could be a range, a single number (flat rate) or “Metered” for TAXI.
        /// </summary>
        [JsonProperty(PropertyName = "estimate")]
        public string Estimate { get; set; }

        /// <summary>
        /// Minimum price for product.
        /// </summary>
        [JsonProperty(PropertyName = "minimum")]
        public int Minimum { get; set; }

        /// <summary>
        /// Expected surge multiplier. Surge is active if surge_multiplier is greater than 1.
        /// Price estimate already factors in the surge multiplier.
        /// </summary>
        [JsonProperty(PropertyName = "surge_multiplier")]
        public float SurgeMultiplier { get; set; }
    }
}