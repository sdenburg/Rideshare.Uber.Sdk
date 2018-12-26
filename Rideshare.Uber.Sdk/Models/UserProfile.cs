using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class UserProfile
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "picture")]
        public string Picture { get; set; }

        [JsonProperty(PropertyName = "promo_code")]
        public string PromoCode { get; set; }
    }
}