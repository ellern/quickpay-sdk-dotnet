using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace QuickPay.SDK.Models.Invoices
{
    public class InvoiceOrder
    {
        /// <summary>
        /// Lines of the order
        /// </summary>
        [JsonPropertyName("lines")]
        public List<InvoiceOrderLine> Lines { get; set; }
    }
}
