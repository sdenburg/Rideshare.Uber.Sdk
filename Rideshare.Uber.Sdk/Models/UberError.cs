using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class UberError
    {
        [JsonProperty(PropertyName = "fields")]
        public Dictionary<string, string> Fields { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
    }
}