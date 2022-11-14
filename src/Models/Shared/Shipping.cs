using System.Text.Json.Serialization;

namespace QuickPay.SDK.Models.Shared
{
    public class Shipping
    {
        /// <summary>
        /// Delivery method
        /// </summary>
        [JsonPropertyName("method")]
        public string Method { get; set; }
        /// <summary>
        /// Delivery company
        /// </summary>
        [JsonPropertyName("company")]
        public string Company { get; set; }
        /// <summary>
        /// elivery price
        /// </summary>
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        /// <summary>
        /// Delivery VAT rate
        /// </summary>
        [JsonPropertyName("vat_rate")]
        public int VatRate { get; set; }
        /// <summary>
        /// Tracking number
        /// </summary>
        [JsonPropertyName("tracking_number")]
        public string TrackingNumber { get; set; }
        /// <summary>
        /// Link to delivery status page
        /// </summary>
        [JsonPropertyName("tracking_url")]
        public string TrackingUrl { get; set; }
    }
}
