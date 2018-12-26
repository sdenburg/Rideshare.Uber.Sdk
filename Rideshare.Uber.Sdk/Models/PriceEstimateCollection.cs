using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class PriceEstimateCollection
    {
        [JsonProperty(PropertyName = "prices")]
        public IList<PriceEstimate> PriceEstimates { get; set; }
    }
}