using System.Text.Json.Serialization;
using System;

namespace QuickPay.SDK.Models.Cards
{
    public class CardToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("is_used")]
        public bool IsUsed { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime Created { get; set; }
    }
}
