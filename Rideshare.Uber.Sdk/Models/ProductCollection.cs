using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rideshare.Uber.Sdk.Models
{
    public class ProductCollection
    {
        [JsonProperty(PropertyName = "products")]
        public IList<Product> Products { get; set; }
    }
}