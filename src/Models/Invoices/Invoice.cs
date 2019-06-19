using Newtonsoft.Json;

namespace QuickPay.Models.Invoices
{
    public class Invoice
    {
        [JsonProperty(PropertyName = "data")]
        public InvoiceData Data { get; set; }
    }
}
