using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class UserActivity
    {
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "history")]
        public IList<UserHistory> History { get; set; }
    }
}