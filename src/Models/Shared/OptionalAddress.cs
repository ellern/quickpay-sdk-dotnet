using System.Text.Json.Serialization;

namespace QuickPay.SDK.Models.Shared
{
    public class OptionalAddress
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("att")]
        public string Att { get; set; }
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("zip_code")]
        public string ZipCode { get; set; }
        [JsonPropertyName("region")]
        public string Region { get; set; }
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
        [JsonPropertyName("vat_no")]
        public string VatNo { get; set; }
        [JsonPropertyName("company_name")]
        public string CompanyName { get; set; }
        [JsonPropertyName("house_number")]
        public string HouseNumber { get; set; }
        [JsonPropertyName("house_extension")]
        public string HouseExtension { get; set; }
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("mobile_number")]
        public string MobileNumber { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
