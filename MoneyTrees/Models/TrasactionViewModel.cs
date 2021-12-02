using Newtonsoft.Json;
using System;

namespace MoneyTrees.Models
{
    public class TrasactionViewModel
    {
        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("emoji")]
        public string Emoji { get; set; }


        [JsonProperty("local_amount")]
        public string LocalAmount { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }

        public string CounterPartName { get; set; }


    }
}