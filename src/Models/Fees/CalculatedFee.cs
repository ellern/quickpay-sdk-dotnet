using Newtonsoft.Json;

namespace QuickPay.SDK.Models.Fees
{
    public class CalculatedFee
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
        /// Amount that the fee is calculated from
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        /// <summary>
        /// The calculated fee in smallest unit
        /// </summary>
        [JsonProperty(PropertyName = "fee")]
        public int Fee { get; set; }

        /// <summary>
        /// Amount + Fee
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
    }
}