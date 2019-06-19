using Newtonsoft.Json;

namespace QuickPay.SDK.Models.Invoices
{
    public class InvoiceOrderLine
    {
        /// <summary>
        /// The product or service
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Further explanation
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public decimal? Quantity { get; set; }

        /// <summary>
        /// The unit that the quantity is measured in
        /// </summary>
        [JsonProperty(PropertyName = "unit")]
        public string Unit { get; set; }

        /// <summary>
        /// Price per unit
        /// </summary>
        [JsonProperty(PropertyName = "unit_price")]
        public int? UnitPrice { get; set; }

        /// <summary>
        /// Tax rate applied to this line
        /// </summary>
        [JsonProperty(PropertyName = "tax_rate")]
        public decimal? TaxRate { get; set; }
    }
}
