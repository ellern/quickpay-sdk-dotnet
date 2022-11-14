using System.Text.Json.Serialization;

namespace QuickPay.SDK.Models.Fees
{
    public class CalculatedFee
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
        /// Amount that the fee is calculated from
        /// </summary>
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// The calculated fee in smallest unit
        /// </summary>
        [JsonPropertyName("fee")]
        public int Fee { get; set; }

        /// <summary>
        /// Amount + Fee
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}