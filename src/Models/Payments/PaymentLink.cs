using System.Text.Json.Serialization;
using QuickPay.SDK.Models.Shared;

namespace QuickPay.SDK.Models.Payments
{
    public class PaymentLink : Link
    {
        /// <summary>
        /// Amount to authorize.
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// If true, will add acquirer fee to the amount
        /// </summary>
        [JsonPropertyName("auto_fee")]
        public bool? AutoFee { get; set; }
        /// <summary>
        /// If true, will capture the transaction after authorize succeeds
        /// </summary>
        [JsonPropertyName("auto_capture")]
        public bool? AutoCapture { get; set; }
        /// <summary>
        /// Deadline in seconds for the cardholder to complete
        /// </summary>
        public string Deadline { get; set; }
        /// <summary>
        /// Get customer invoice address via acquirer (Currently MobilePay and PayPal only)
        /// </summary>
        [JsonPropertyName("invoice_address_selection")]
        public bool? InvoiceAddressSelection { get; set; }
        /// <summary>
        /// Get customer shipping address via acquirer (Currently MobilePay and PayPal only)
        /// </summary>
        [JsonPropertyName("shipping_address_selection")]
        public bool? ShippingAddressSelection { get; set; }
        /// <summary>
        /// PayPal specific: Customer email
        /// </summary>
        [JsonPropertyName("customer_email")]
        public string CustomerEmail { get; set; }
    }
}
