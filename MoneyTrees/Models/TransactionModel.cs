using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace MoneyTrees.Models
{
 
    public  class TransactionModel
    {

        [JsonProperty("transaction")]
        public TransactionModel Transaction { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        //[JsonProperty("fees")]
        //public object Fees { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("merchant")]
        public MerchantModel Merchant { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("metadata")]
        public MetadataModel Metadata { get; set; }

        //[JsonProperty("labels")]
        //public object Labels { get; set; }

        [JsonProperty("account_balance")]
        public decimal AccountBalance { get; set; }

        //[JsonProperty("attachments")]
        //public object[] Attachments { get; set; }

        [JsonProperty("international")]
        public object International { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("is_load")]
        public bool IsLoad { get; set; }

        [JsonProperty("settled")]
        public string Settled { get; set; }

        [JsonProperty("local_amount")]
        public double LocalAmount { get; set; }

        [JsonProperty("local_currency")]
        public string LocalCurrency { get; set; }

        [JsonProperty("updated")]
        public DateTimeOffset Updated { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("counterparty")]
        public CounterpartyModel Counterparty { get; set; }

        [JsonProperty("scheme")]
        public string Scheme { get; set; }

        [JsonProperty("dedupe_id")]
        public string DedupeId { get; set; }

        [JsonProperty("originator")]
        public bool Originator { get; set; }

        [JsonProperty("include_in_spending")]
        public bool IncludeInSpending { get; set; }

        [JsonProperty("can_be_excluded_from_breakdown")]
        public bool CanBeExcludedFromBreakdown { get; set; }

        [JsonProperty("can_be_made_subscription")]
        public bool CanBeMadeSubscription { get; set; }

        [JsonProperty("can_split_the_bill")]
        public bool CanSplitTheBill { get; set; }

        [JsonProperty("can_add_to_tab")]
        public bool CanAddToTab { get; set; }

        [JsonProperty("amount_is_pending")]
        public bool AmountIsPending { get; set; }

        [JsonProperty("decline_reason", NullValueHandling = NullValueHandling.Ignore)]
        public string DeclineReason { get; set; }
    }

 }
