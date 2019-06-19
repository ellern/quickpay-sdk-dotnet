using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuickPay.SDK.Models.Invoices
{
    public class InvoiceOrder
    {
        /// <summary>
        /// Lines of the order
        /// </summary>
        [JsonProperty(PropertyName = "lines")]
        public List<InvoiceOrderLine> Lines { get; set; }
    }
}
