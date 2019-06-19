using Newtonsoft.Json;
using System;

namespace QuickPay.Models.Invoices
{
    public class InvoiceAttributes
    {
        /// <summary>
        /// Id of the customer this invoice belongs to
        /// </summary>
        [JsonProperty(PropertyName = "customer_id")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Invoice number
        /// </summary>
        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        /// <summary>
        /// Total amount of invoice including VAT
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Total amount of invoice, but excluding VAT
        /// </summary>
        [JsonProperty(PropertyName = "amount_without_vat")]
        public int AmountWithoutVAT { get; set; }

        /// <summary>
        /// Three letter currency code
        /// </summary>
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Due date
        /// </summary>
        [JsonProperty(PropertyName = "due_at")]
        public DateTime? Due { get; set; }

        /// <summary>
        /// Time of payment
        /// </summary>
        [JsonProperty(PropertyName = "paid_at")]
        public DateTime? Paid { get; set; }

        /// <summary>
        /// The time of the last failed payment attempt
        /// </summary>
        [JsonProperty(PropertyName = "payment_failed_at")]
        public DateTime? Failed { get; set; }

        /// <summary>
        /// Order specification
        /// </summary>
        [JsonProperty(PropertyName = "order")]
        public InvoiceOrder Order { get; set; }

        /// <summary>
        /// If set, this invoice is not processed automatically. In that case paid_at might not be set even if the invoice is paid.
        /// </summary>
        [JsonProperty(PropertyName = "manual")]
        public bool Manual { get; set; }
    }
}
