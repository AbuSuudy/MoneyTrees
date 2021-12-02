using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTrees.Models
{
    public class CategorySum
    {
        [JsonProperty("name")]
        public string Category { get; set; }

        [JsonProperty("y")]
        public double Amount { get; set; }
    
    }
}