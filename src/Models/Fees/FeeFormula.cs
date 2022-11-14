using System.Text.Json.Serialization;

namespace QuickPay.SDK.Models.Fees
{
    public class FeeFormula
    {
        /// <summary>
        /// The formula
        /// </summary>
        [JsonPropertyName("formula")]
        public string Formula { get; set; }

        /// <summary>
        /// Acquirer
        /// </summary>
        [JsonPropertyName("acquirer")]
        public string Acquirer { get; set; }

        /// <summary>
        /// Payment method
        /// </summary>
        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// True if this is the standard fee formula
        /// </summary>
        [JsonPropertyName("standard")]
        public bool Standard { get; set; }
    }
}