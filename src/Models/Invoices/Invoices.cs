using System.Text.Json.Serialization;

namespace QuickPay.SDK.Models.Invoices
{
    public class Invoices
    {
        [JsonPropertyName("data")]
        public InvoiceData[] Data { get; set; }
    }
}
