using Newtonsoft.Json;

namespace MoneyTrees.Models
{
    public class WhoAmIModel
    {
        [JsonProperty("authenticated")]
        public bool Authenticated { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}