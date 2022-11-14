using System.Text.Json.Serialization;
using QuickPay.SDK.Models.Payments;
using QuickPay.SDK.Models.Shared;
using System;
using System.Collections.Generic;

namespace QuickPay.SDK.Models.Subscriptions
{
    public class Subscription
    {
        public int Id { get; set; }
        /// <summary>
        /// Merchant Id
        /// </summary>
        [JsonPropertyName("merchant_id")]
        public int MerchantId { get; set; }

        /// <summary>
        /// Order number
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        /// <summary>
        /// Accepted by acquirer boolean
        /// </summary>
        public bool Accepted { get; set; }

        /// <summary>
        /// Transaction type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Text on statement
        /// </summary>
        [JsonPropertyName("text_on_statement")]
        public string Descriptor { get; set; }

        /// <summary>
        /// Branding Id
        /// </summary>
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
        /// State of transaction - e.g. initial, pending, new, rejected, processed
        /// </summary>
        public string State { get; set; }

        //metadata Metadata    Metadata
        public Metadata Metadata { get; set; }

        //link    PaymentLink PaymentLink
        public PaymentLink PaymentLink { get; set; }

        //shipping_address Shipping address set on payment creation OptionalAddress
        [JsonPropertyName("shipping_address")]
        public OptionalAddress ShippingAddress { get; set; }

        //invoice_address Invoice address set on payment creation OptionalAddress
        [JsonPropertyName("invoice_address")]
        public OptionalAddress InvoiceAddress { get; set; }

        /// <summary>
        /// Order items
        /// </summary>
        public Basket[] Basket { get; set; }

        /// <summary>
        /// Shipping
        /// </summary>
        public Shipping Shipping { get; set; }

        /// <summary>
        /// Operations
        /// </summary>
        public Operation[] Operations { get; set; }

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
        /// Timestamp of creation
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Timestamp of last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Timestamp of retention
        /// </summary>
        [JsonPropertyName("retented_at")]
        public DateTime? Retented { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ids of subscription groups
        /// </summary>
        [JsonPropertyName("group_ids")]
        public int[] GroupIds { get; set; }

        /// <summary>
        /// Authorize deadline  ISO-8601
        /// </summary>
        [JsonPropertyName("deadline_at")]
        public DateTime? Deadline { get; set; }
    }
}
