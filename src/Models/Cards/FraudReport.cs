using System.Text.Json.Serialization;
using System;

namespace QuickPay.SDK.Models.Cards
{
    public class FraudReport
    {
        public string Description { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime Created { get; set; }
    }
}
