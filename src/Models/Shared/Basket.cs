using Newtonsoft.Json;

namespace QuickPay.SDK.Models.Shared
{
    public class Basket
    {
        [JsonProperty(PropertyName = "qty")]
        public int Quantity { get; set; }
        [JsonProperty(PropertyName = "item_no")]
        public string ItemNo { get; set; }
        [JsonProperty(PropertyName = "item_name")]
        public string ItemName { get; set; }
        [JsonProperty(PropertyName = "item_price")]
        public int ItemPrice { get; set; }
        [JsonProperty(PropertyName = "vat_rate")]
        public int VatRate { get; set; }
    }
}
