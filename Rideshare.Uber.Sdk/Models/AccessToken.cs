using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class AccessToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Amount of time in seconds until Value expires
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Valid for one year
        /// </summary>
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }
    }
}