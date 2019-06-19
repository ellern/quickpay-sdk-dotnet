using Newtonsoft.Json;
using QuickPay.Models.Shared;

namespace QuickPay.Models.Payments
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
        [JsonProperty(PropertyName = "auto_fee")]
        public bool? AutoFee { get; set; }
        /// <summary>
        /// If true, will capture the transaction after authorize succeeds
        /// </summary>
        [JsonProperty(PropertyName = "auto_capture")]
        public bool? AutoCapture { get; set; }
        /// <summary>
        /// Deadline in seconds for the cardholder to complete
        /// </summary>
        public string Deadline { get; set; }
        /// <summary>
        /// Get customer invoice address via acquirer (Currently MobilePay and PayPal only)
        /// </summary>
        [JsonProperty(PropertyName = "invoice_address_selection")]
        public bool? InvoiceAddressSelection { get; set; }
        /// <summary>
        /// Get customer shipping address via acquirer (Currently MobilePay and PayPal only)
        /// </summary>
        [JsonProperty(PropertyName = "shipping_address_selection")]
        public bool? ShippingAddressSelection { get; set; }
        /// <summary>
        /// PayPal specific: Customer email
        /// </summary>
        [JsonProperty(PropertyName = "customer_email")]
        public string CustomerEmail { get; set; }
    }
}
