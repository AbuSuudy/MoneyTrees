using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTrees.Models
{
    public class CounterpartyModel
    {
        [JsonProperty("account_number", NullValueHandling = NullValueHandling.Ignore)]
        public long? AccountNumber { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("sort_code", NullValueHandling = NullValueHandling.Ignore)]
        public long? SortCode { get; set; }

        [JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

     }
}