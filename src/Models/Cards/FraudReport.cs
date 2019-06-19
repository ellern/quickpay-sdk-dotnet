using Newtonsoft.Json;
using System;

namespace QuickPay.Models.Cards
{
    public class FraudReport
    {
        public string Description { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime Created { get; set; }
    }
}
