using System.Text.Json.Serialization;
using System;

namespace QuickPay.SDK.Models.Invoices
{
    public class InvoiceAttributes
    {
        /// <summary>
        /// Id of the customer this invoice belongs to
        /// </summary>
        [JsonPropertyName("customer_id")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Invoice number
        /// </summary>
        [JsonPropertyName("number")]
        public string Number { get; set; }

        /// <summary>
        /// Total amount of invoice including VAT
        /// </summary>
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Total amount of invoice, but excluding VAT
        /// </summary>
        [JsonPropertyName("amount_without_vat")]
        public int AmountWithoutVAT { get; set; }

        /// <summary>
        /// Three letter currency code
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Due date
        /// </summary>
        [JsonPropertyName("due_at")]
        public DateTime? Due { get; set; }

        /// <summary>
        /// Time of payment
        /// </summary>
        [JsonPropertyName("paid_at")]
        public DateTime? Paid { get; set; }

        /// <summary>
        /// The time of the last failed payment attempt
        /// </summary>
        [JsonPropertyName("payment_failed_at")]
        public DateTime? Failed { get; set; }

        /// <summary>
        /// Order specification
        /// </summary>
        [JsonPropertyName("order")]
        public InvoiceOrder Order { get; set; }

        /// <summary>
        /// If set, this invoice is not processed automatically. In that case paid_at might not be set even if the invoice is paid.
        /// </summary>
        [JsonPropertyName("manual")]
        public bool Manual { get; set; }
    }
}
