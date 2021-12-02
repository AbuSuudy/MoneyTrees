using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTrees.Models
{
    public class SingleTransactionItemModel
    {
        [JsonProperty("transaction")]
        public TransactionModel Transaction { get; set; }
    }
}