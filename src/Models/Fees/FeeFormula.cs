using Newtonsoft.Json;

namespace QuickPay.Models.Fees
{
    public class FeeFormula
    {
        /// <summary>
        /// The formula
        /// </summary>
        [JsonProperty(PropertyName = "formula")]
        public string Formula { get; set; }

        /// <summary>
        /// Acquirer
        /// </summary>
        [JsonProperty(PropertyName = "acquirer")]
        public string Acquirer { get; set; }

        /// <summary>
        /// Payment method
        /// </summary>
        [JsonProperty(PropertyName = "payment_method")]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// True if this is the standard fee formula
        /// </summary>
        [JsonProperty(PropertyName = "standard")]
        public bool Standard { get; set; }
    }
}