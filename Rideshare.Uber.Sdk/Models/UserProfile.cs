using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class UserProfile
    {
        /// <summary>
        /// Image URL of the Uber user.
        /// </summary>
        [JsonProperty(PropertyName = "picture")]
        public string Picture { get; set; }

        /// <summary>
        /// First name of the Uber user.
        /// </summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the Uber user.
        /// </summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Unique identifier of the Uber user.
        /// </summary>
        [JsonProperty(PropertyName = "uuid")]
        public string UserId { get; set; }

        /// <summary>
        /// Encrypted unique identifier of the Uber rider.
        /// </summary>
        [JsonProperty(PropertyName = "rider_id")]
        public string RiderId { get; set; }

        /// <summary>
        /// Email address of the Uber user.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Whether the user has confirmed their mobile number.
        /// </summary>
        [JsonProperty(PropertyName = "mobile_verified")]
        public bool MobileVerified { get; set; }

        /// <summary>
        /// The promotion code for the user. Can be used for rewards when referring other users to Uber
        /// </summary>
        [JsonProperty(PropertyName = "promo_code")]
        public string PromoCode { get; set; }
    }
}