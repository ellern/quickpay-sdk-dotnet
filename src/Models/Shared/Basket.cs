using System.Text.Json.Serialization;

namespace QuickPay.SDK.Models.Shared
{
    public class Basket
    {
        [JsonPropertyName("qty")]
        public int Quantity { get; set; }
        [JsonPropertyName("item_no")]
        public string ItemNo { get; set; }
        [JsonPropertyName("item_name")]
        public string ItemName { get; set; }
        [JsonPropertyName("item_price")]
        public int ItemPrice { get; set; }
        [JsonPropertyName("vat_rate")]
        public int VatRate { get; set; }
    }
}
