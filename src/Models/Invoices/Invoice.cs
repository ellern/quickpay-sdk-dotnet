using System.Text.Json.Serialization;

namespace QuickPay.SDK.Models.Invoices
{
    public class Invoice
    {
        [JsonPropertyName("data")]
        public InvoiceData Data { get; set; }
    }
}
