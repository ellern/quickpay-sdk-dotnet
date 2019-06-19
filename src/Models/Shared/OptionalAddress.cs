using Newtonsoft.Json;

namespace QuickPay.Models.Shared
{
    public class OptionalAddress
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "att")]
        public string Att { get; set; }
        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "zip_code")]
        public string ZipCode { get; set; }
        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }
        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode { get; set; }
        [JsonProperty(PropertyName = "vat_no")]
        public string VatNo { get; set; }
        [JsonProperty(PropertyName = "company_name")]
        public string CompanyName { get; set; }
        [JsonProperty(PropertyName = "house_number")]
        public string HouseNumber { get; set; }
        [JsonProperty(PropertyName = "house_extension")]
        public string HouseExtension { get; set; }
        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty(PropertyName = "mobile_number")]
        public string MobileNumber { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
