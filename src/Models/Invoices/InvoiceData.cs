using Newtonsoft.Json;

namespace QuickPay.SDK.Models.Invoices
{
    public class InvoiceData
    {
        /// <summary>
        /// Unique id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Type of this entity. Together with the Id, this forms a unique key across all resources
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "attributes")]
        public InvoiceAttributes Attributes { get; set; }
    }
}
