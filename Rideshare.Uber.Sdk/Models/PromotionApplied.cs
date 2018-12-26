using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class PromotionApplied
    {
        /// <summary>
        /// The applied promotion code.
        /// </summary>
        [JsonProperty(PropertyName = "promotion_code")]
        public string PromotionCode { get; set; }

        /// <summary>
        /// A brief description of the promotion.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
