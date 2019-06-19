using Newtonsoft.Json;
using QuickPay.Models.Shared;
using System;
using System.Collections.Generic;

namespace QuickPay.Models.Callbacks
{
    public class Callback
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "merchant_id")]
        public int MerchantId { get; set; }

        [JsonProperty(PropertyName = "order_id")]
        public string OrderId { get; set; }

        public bool Accepted { get; set; }

        public string Type { get; set; }

        [JsonProperty(PropertyName = "text_on_statement")]
        public string Descriptor { get; set; }

        [JsonProperty(PropertyName = "Branding_id")]
        public string BrandingId { get; set; }

        public Dictionary<string, string> Variables { get; set; }

        public string Currency { get; set; }

        public string State { get; set; }

        public Metadata Metadata { get; set; }

        public Link Link { get; set; }

        [JsonProperty(PropertyName = "shipping_address")]
        public OptionalAddress ShippingAddress { get; set; }

        [JsonProperty(PropertyName = "invoice_address")]
        public OptionalAddress InvoiceAddress { get; set; }

        public Basket[] Basket { get; set; }

        public Shipping Shipping { get; set; }

        public Operation[] Operations { get; set; }

        [JsonProperty(PropertyName = "test_mode")]
        public bool TestMode { get; set; }

        public string Acquirer { get; set; }

        public string Facilitator { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime? Updated { get; set; }

        [JsonProperty(PropertyName = "retented_at")]
        public DateTime? Retented { get; set; }

        public int Balance { get; set; }

        public int? Fee { get; set; }

        [JsonProperty(PropertyName = "deadline_at")]
        public DateTime? Deadline { get; set; }

        [JsonIgnore]
        public bool TypeIsPayment { get { return Type.Equals("payment", StringComparison.OrdinalIgnoreCase); } }

        [JsonIgnore]
        public bool TypeIsCard { get { return Type.Equals("card", StringComparison.OrdinalIgnoreCase); } }

        [JsonIgnore]
        public bool TypeIsSubscription { get { return Type.Equals("subscription", StringComparison.OrdinalIgnoreCase); } }
    }
}
