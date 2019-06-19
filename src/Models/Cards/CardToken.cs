using Newtonsoft.Json;
using System;

namespace QuickPay.Models.Cards
{
    public class CardToken
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
        [JsonProperty(PropertyName = "is_used")]
        public bool IsUsed { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime Created { get; set; }
    }
}
