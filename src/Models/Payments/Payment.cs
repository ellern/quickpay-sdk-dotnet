using System.Text.Json.Serialization;
using QuickPay.SDK.Models.Shared;
using System;
using System.Collections.Generic;

namespace QuickPay.SDK.Models.Payments
{
    public class Payment
    {
        public int Id { get; set; }

        /// <summary>
        /// Merchant ID
        /// </summary>
        [JsonPropertyName("merchant_id")]
        public int MerchantId { get; set; }

        /// <summary>
        /// Order number
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// Accepted by acquirer
        /// </summary>
        public bool Accepted { get; set; }

        /// <summary>
        /// Transaction type, e.g. 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Text on statement
        /// </summary>
        [JsonPropertyName("text_on_statement")]
        public string Descriptor { get; set; }

        [JsonPropertyName("branding_id")]
        public int? BrandingId { get; set; }

        /// <summary>
        /// Custom variables
        /// </summary>
        public Dictionary<string, string> Variables { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// State of transaction (initial, pending, new, rejected, processed)
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Metadata
        /// </summary>
        public PaymentMetadata Metadata { get; set; }

        /// <summary>
        /// Payment link
        /// </summary>
        public PaymentLink Link { get; set; }

        /// <summary>
        /// Shipping address set on payment creation. Optional.
        /// </summary>
        [JsonPropertyName("shipping_address")]
        public OptionalAddress ShippingAddress { get; set; }

        /// <summary>
        /// Invoice address set on payment creation. Optional.
        /// </summary>
        [JsonPropertyName("invoice_address")]
        public OptionalAddress InvoiceAddress { get; set; }

        //basket Order items Basket
        public Basket[] Basket { get; set; }

        //shipping Shipping    Shipping
        public Shipping Shipping { get; set; }

        /// <summary>
        /// Operations
        /// </summary>
        public PaymentOperation[] Operations { get; set; }

        /// <summary>
        /// Test mode
        /// </summary>
        [JsonPropertyName("test_mode")]
        public bool TestMode { get; set; }

        /// <summary>
        /// Acquirer that processed the transaction
        /// </summary>
        public string Acquirer { get; set; }

        /// <summary>
        /// Facilitator that facilitated the transaction
        /// </summary>
        public string Facilitator { get; set; }

        /// <summary>
        /// Timestamp of creation ISO-8601
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Timestamp of last updated ISO-8601
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Timestamp of retention  ISO-8601
        /// </summary>
        [JsonPropertyName("retented_at")]
        public DateTime? Retented { get; set; }

        /// <summary>
        /// Balance integer
        /// </summary>
        public int Balance { get; set; }
        
        /// <summary>
        /// Fee added to authorization amount(only relevant on auto-fee)
        /// </summary>
        public int? Fee { get; set; }

        /// <summary>
        /// Parent subscription id(only recurring) integer
        /// </summary>
        [JsonPropertyName("subscription_id")]
        public int? SubscriptionId { get; set; }

        /// <summary>
        /// Authorize deadline  ISO-8601
        /// </summary>
        [JsonPropertyName("deadline_at")]
        public DateTime? Deadline { get; set; }
    }
}
