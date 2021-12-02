using Newtonsoft.Json;

namespace MoneyTrees.Models
{
    public class BalanceModel
    {

        [JsonProperty("balance")]
        public long Balance { get; set; }

        [JsonProperty("total_balance")]
        public long TotalBalance { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("spend_today")]
        public long SpendToday { get; set; }

    }
}