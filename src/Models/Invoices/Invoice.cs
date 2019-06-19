using Newtonsoft.Json;

namespace QuickPay.SDK.Models.Invoices
{
    public class Invoice
    {
        [JsonProperty(PropertyName = "data")]
        public InvoiceData Data { get; set; }
    }
}
