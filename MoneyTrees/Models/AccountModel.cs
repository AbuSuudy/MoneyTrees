using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoneyTrees.Models
{
    public class AccountModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("owners")]
        public List<OwnerModel> Owners { get; set; }

        [JsonProperty("account_number")]
        public long AccountNumber { get; set; }

        [JsonProperty("sort_code")]
        public string SortCode { get; set; }

        [JsonProperty("payment_details")]
        public PaymentDetailsModel PaymentDetails { get; set; }

    }
}