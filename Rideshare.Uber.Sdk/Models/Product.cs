using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class Product
    {
        /// <summary>
        /// Unique identifier representing a specific product for a given latitude and longitude.
        /// For example, uberX in San Francisco will have a different product_id than uberX in Los Angeles.
        /// </summary>
        [JsonProperty(PropertyName = "product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// The product group that this product belongs to.
        /// One of rideshare,uberx,uberxl,uberblack, suv, or taxi.
        /// </summary>
        [JsonProperty(PropertyName = "product_group")]
        public string ProductGroup { get; set; }

        /// <summary>
        /// The basic price details (not including any surge pricing adjustments).
        /// This field is null for products with metered fares (taxi) or upfront fares (uberPOOL).
        /// </summary>
        [JsonProperty(PropertyName = "price_details")]
        public PriceDetails PriceDetails { get; set; }

        /// <summary>
        /// Specifies whether this product allows cash payments
        /// </summary>
        [JsonProperty(PropertyName = "cash_enabled")]
        public bool CashEnabled { get; set; }

        /// <summary>
        /// Specifies whether this product allows for the pickup and drop off of other riders during the trip
        /// </summary>
        [JsonProperty(PropertyName = "shared")]
        public bool Shared { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// An abbreviated description of the product. Localized according to Accept-Language header.
        /// </summary>
        [JsonProperty(PropertyName = "short_description")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Product display name
        /// </summary>
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Product capacity (ex. 4 people)
        /// </summary>
        [JsonProperty(PropertyName = "capacity")]
        public int Capacity { get; set; }

        /// <summary>
        /// Image URL representing the product
        /// </summary>
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }
    }
}