using Newtonsoft.Json;

namespace QuickPay.SDK.Models.Invoices
{
    public class Invoices
    {
        [JsonProperty(PropertyName = "data")]
        public InvoiceData[] Data { get; set; }
    }
}
