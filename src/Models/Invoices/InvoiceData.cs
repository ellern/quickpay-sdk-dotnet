using System.Text.Json.Serialization;

namespace QuickPay.SDK.Models.Invoices
{
    public class InvoiceData
    {
        /// <summary>
        /// Unique id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Type of this entity. Together with the Id, this forms a unique key across all resources
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("attributes")]
        public InvoiceAttributes Attributes { get; set; }
    }
}
