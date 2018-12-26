using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class TimeEstimateCollection
    {
        [JsonProperty(PropertyName = "times")]
        public IList<TimeEstimate> TimeEstimates { get; set; }
    }
}