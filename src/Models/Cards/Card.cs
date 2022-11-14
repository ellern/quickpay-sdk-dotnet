using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace QuickPay.SDK.Models.Cards
{
    public class Card
    {
        /// <summary>
        /// Card id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Merchant id
        /// </summary>
        [JsonPropertyName("merchant_id")]
        public int MerchantId { get; set; }
        /// <summary>
        /// Accepted by acquirer
        /// </summary>
        public bool Accepted { get; set; }
        /// <summary>
        /// Operations
        /// </summary>
        public CardOperation[] Operations { get; set; }
        /// <summary>
        /// Card metadata
        /// </summary>
        public CardMetadata Metadata { get; set; }
        /// <summary>
        /// Sharable link to payment window
        /// </summary>
        public CardLink Link { get; set; }
        /// <summary>
        /// Custom variables
        /// </summary>
        [JsonPropertyName("variables")]
        public Dictionary<string, string> Variables { get; set; }
        /// <summary>
        /// Test mode
        /// </summary>
        [JsonPropertyName("test_mode")]
        public bool TestMode { get; set; }
        /// <summary>
        /// Acquirer that processed the card
        /// </summary>
        public string Acquirer { get; set; }
        /// <summary>
        /// Type of transaction
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Timestamp of creation
        /// </summary>
        public DateTime Created { get; set; }
    }
}
