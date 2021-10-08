using Newtonsoft.Json;
using System;

namespace QuickPay.SDK.Models.Shared
{
    public class Metadata
    {
        /// <summary>
        /// Type - card, mobile, nin
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Origin of this transaction or card. If set, describes where it came from.
        /// </summary>
        public string Origin { get; set; }
        /// <summary>
        /// Card type only: The card brand
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// Card type only: Card BIN
        /// </summary>
        public string Bin { get; set; }
        /// <summary>
        /// Card type only: Corporate status
        /// </summary>
        [JsonProperty(PropertyName = "corporate")]
        public bool? IsCorporate { get; set; }
        /// <summary>
        /// Card type only: The last 4 digits of the card number
        /// </summary>
        public string Last4 { get; set; }
        /// <summary>
        /// Card type only: The expiration month
        /// </summary>
        [JsonProperty(PropertyName = "exp_month")]
        public int? ExpiryMonth { get; set; }
        /// <summary>
        /// Card type only: The expiration year (YYYY)
        /// </summary>
        [JsonProperty(PropertyName = "exp_year")]
        public int? ExpiryYear { get; set; }
        /// <summary>
        /// Card type only: The card country in ISO 3166-1 alpha-3
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Card type only: Verified via 3D-Secure
        /// </summary>
        [JsonProperty(PropertyName = "is_3d_secure")]
        public bool? Is3DSecure { get; set; }
        /// <summary>
        /// Name of cardholder
        /// </summary>
        [JsonProperty(PropertyName = "issued_to")]
        public string IssuedTo { get; set; }
        /// <summary>
        /// Card type only: PCI safe hash of card number
        /// </summary>
        public string Hash { get; set; }
        /// <summary>
        /// Mobile type only: The mobile number
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Customer IP
        /// </summary>
        [JsonProperty(PropertyName = "customer_ip")]
        public string CustomerIPAddress { get; set; }
        /// <summary>
        /// Customer country based on IP geo-data, ISO 3166-1 alpha-2
        /// </summary>
        [JsonProperty(PropertyName = "customer_country")]
        public string CustomerCountry { get; set; }
        /// <summary>
        /// Suspected fraud
        /// </summary>
        [JsonProperty(PropertyName = "fraud_suspected")]
        public bool FraudSuspected { get; set; }
        /// <summary>
        /// Fraud remarks
        /// </summary>
        [JsonProperty(PropertyName = "fraud_remarks")]
        public string[] FraudRemarks { get; set; }
        /// <summary>
        /// Reported as fraudulent
        /// </summary>
        [JsonProperty(PropertyName = "fraud_reported")]
        public bool FraudReported { get; set; }
        /// <summary>
        /// Fraud report description
        /// </summary>
        [JsonProperty(PropertyName = "fraud_report_description")]
        public string FraudReportDescription { get; set; }
        /// <summary>
        /// Fraud reported at
        /// </summary>
        [JsonProperty(PropertyName = "fraud_reported_at")]
        public DateTime? FraudReportedAt { get; set; }
        /// <summary>
        /// NIN type only. NIN number
        /// </summary>
        [JsonProperty(PropertyName = "nin_number")]
        public string NINNumber { get; set; }
        /// <summary>
        /// NIN type only. NIN country code, ISO 3166-1 alpha-3
        /// </summary>
        [JsonProperty(PropertyName = "nin_country_code")]
        public string NINCountryCode { get; set; }
        /// <summary>
        /// NIN type only. NIN gender
        /// </summary>
        [JsonProperty(PropertyName = "nin_gender")]
        public string NINGender { get; set; }
        /// <summary>
        /// Shop system module name
        /// </summary>
        [JsonProperty(PropertyName = "shopsystem_name")]
        public string ShopSystemName { get; set; }
        /// <summary>
        /// Shop system module version
        /// </summary>
        [JsonProperty(PropertyName = "shopsystem_version")]
        public string ShopSystemVersion { get; set; }

    }
}
