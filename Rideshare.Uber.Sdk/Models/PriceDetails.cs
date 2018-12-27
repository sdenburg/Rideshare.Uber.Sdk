using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class PriceDetails
    {
        /// <summary>
        /// The base price
        /// </summary>
        [JsonProperty(PropertyName = "base")]
        public float Base { get; set; }

        /// <summary>
        /// The minimum price of a trip
        /// </summary>
        [JsonProperty(PropertyName = "minimum")]
        public float? Minimum { get; set; }

        /// <summary>
        /// The charge per minute (if applicable for the product type)
        /// </summary>
        [JsonProperty(PropertyName = "cost_per_minute")]
        public float CostPerMinute { get; set; }

        /// <summary>
        /// The charge per distance unit (if applicable for the product type)
        /// </summary>
        [JsonProperty(PropertyName = "cost_per_distance")]
        public float CostPerDistance { get; set; }

        /// <summary>
        /// The unit of distance used to calculate the fare (either mile or km)
        /// </summary>
        [JsonProperty(PropertyName = "distance_unit")]
        public string DistanceUnit { get; set; }

        /// <summary>
        /// The fee if a rider cancels the trip after the grace period
        /// </summary>
        [JsonProperty(PropertyName = "cancellation_fee")]
        public float CancellationFee { get; set; }

        /// <summary>
        /// ISO 4217 currency code
        /// </summary>
        [JsonProperty(PropertyName = "currency_code")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Array containing additional fees added to the price of a product
        /// </summary>
        [JsonProperty(PropertyName = "service_fees")]
        public IList<ServiceFees> ServiceFees { get; set; }
    }
}