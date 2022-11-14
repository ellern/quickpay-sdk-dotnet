using System.Text.Json.Serialization;
using QuickPay.SDK.Models.Shared;
using System;
using System.Collections.Generic;

namespace QuickPay.SDK.Models.Callbacks
{
    public class Callback
    {
        public int Id { get; set; }

        /// <summary>
        /// Universally-Unique, Lexicographically-Sortable Identifier
        /// </summary>                
        public string ULId { get; set; }

        [JsonPropertyName("merchant_id")]
        public int MerchantId { get; set; }

        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        public bool Accepted { get; set; }

        public string Type { get; set; }

        [JsonPropertyName("text_on_statement")]
        public string Descriptor { get; set; }

        [JsonPropertyName("Branding_id")]
        public string BrandingId { get; set; }

        public Dictionary<string, string> Variables { get; set; }

        public string Currency { get; set; }

        public string State { get; set; }

        public Metadata Metadata { get; set; }

        public Link Link { get; set; }

        [JsonPropertyName("shipping_address")]
        public OptionalAddress ShippingAddress { get; set; }

        [JsonPropertyName("invoice_address")]
        public OptionalAddress InvoiceAddress { get; set; }

        public Basket[] Basket { get; set; }

        public Shipping Shipping { get; set; }

        public Operation[] Operations { get; set; }

        [JsonPropertyName("test_mode")]
        public bool TestMode { get; set; }

        public string Acquirer { get; set; }

        public string Facilitator { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime Created { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? Updated { get; set; }

        [JsonPropertyName("retented_at")]
        public DateTime? Retented { get; set; }

        public int Balance { get; set; }

        public int? Fee { get; set; }

        [JsonPropertyName("deadline_at")]
        public DateTime? Deadline { get; set; }

        [JsonIgnore]
        public bool TypeIsPayment { get { return Type.Equals("payment", StringComparison.OrdinalIgnoreCase); } }

        [JsonIgnore]
        public bool TypeIsCard { get { return Type.Equals("card", StringComparison.OrdinalIgnoreCase); } }

        [JsonIgnore]
        public bool TypeIsSubscription { get { return Type.Equals("subscription", StringComparison.OrdinalIgnoreCase); } }
    }
}
