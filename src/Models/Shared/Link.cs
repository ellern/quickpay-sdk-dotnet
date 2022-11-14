using System.Text.Json.Serialization;

namespace QuickPay.SDK.Models.Shared
{
    public class Link
    {
        /// <summary>
        /// Url to payment window for this payment link
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// Id of agreement that will be used in the payment window
        /// </summary>
        [JsonPropertyName("agreement_id")]
        public int AgreementId { get; set; }
        
        /// <summary>
        /// Two letter language code that determines the language of the payment window
        /// </summary>
        public string Language { get; set; }
        
        /// <summary>
        /// Where cardholder is redirected after success
        /// </summary>
        public string ContinueUrl { get; set; }
        
        /// <summary>
        /// Where cardholder is redirected after cancel
        /// </summary>
        public string CancelUrl { get; set; }
        
        /// <summary>
        /// Endpoint for a POST callback
        /// </summary>
        public string CallbackUrl { get; set; }
        
        /// <summary>
        /// Lock to these payment methods
        /// </summary>
        [JsonPropertyName("payment_methods")]
        public string PaymentMethods { get; set; }
        
        /// <summary>
        /// The branding to use in the payment window
        /// </summary>
        [JsonPropertyName("branding_id")]
        public int? BrandingId { get; set; }
        
        /// <summary>
        /// Set this to enable Google Analytics events from the payment window
        /// </summary>
        [JsonPropertyName("google_analytics_client_id")]
        public string GoogleAnalyticsClientId { get; set; }
        
        /// <summary>
        /// Set this to enable Google Analytics events from the payment window
        /// </summary>
        [JsonPropertyName("google_analytics_tracking_id")]
        public string GoogleAnalyticsTrackingId { get; set; }
        
        /// <summary>
        /// Version of payment window and API
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// Force usage of this acquirer
        /// </summary>
        public string Acquirer { get; set; }
        
        /// <summary>
        /// Allowed in iframe
        /// </summary>
        public bool Framed { get; set; }
        
        /// <summary>
        /// Branding config
        /// </summary>
        [JsonPropertyName("branding_config")]
        public object BrandingConfig { get; set; }
    }
}
