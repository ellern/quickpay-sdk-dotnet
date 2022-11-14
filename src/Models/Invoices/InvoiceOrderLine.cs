using System.Text.Json.Serialization;

namespace QuickPay.SDK.Models.Invoices
{
    public class InvoiceOrderLine
    {
        /// <summary>
        /// The product or service
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Further explanation
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal? Quantity { get; set; }

        /// <summary>
        /// The unit that the quantity is measured in
        /// </summary>
        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        /// <summary>
        /// Price per unit
        /// </summary>
        [JsonPropertyName("unit_price")]
        public int? UnitPrice { get; set; }

        /// <summary>
        /// Tax rate applied to this line
        /// </summary>
        [JsonPropertyName("tax_rate")]
        public decimal? TaxRate { get; set; }
    }
}
