using Newtonsoft.Json;

namespace QuickPay.Models.Shared
{
    public class Shipping
    {
        /// <summary>
        /// Delivery method
        /// </summary>
        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }
        /// <summary>
        /// Delivery company
        /// </summary>
        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }
        /// <summary>
        /// elivery price
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }
        /// <summary>
        /// Delivery VAT rate
        /// </summary>
        [JsonProperty(PropertyName = "vat_rate")]
        public int VatRate { get; set; }
        /// <summary>
        /// Tracking number
        /// </summary>
        [JsonProperty(PropertyName = "tracking_number")]
        public string TrackingNumber { get; set; }
        /// <summary>
        /// Link to delivery status page
        /// </summary>
        [JsonProperty(PropertyName = "tracking_url")]
        public string TrackingUrl { get; set; }
    }
}
