using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace QuickPay.SDK.Models.Shared
{
    public class Pong
    {
        /// <summary>
        /// Friendly message
        /// </summary>
        [JsonPropertyName("msg")]
        public string Message { get; set; }
        /// <summary>
        /// API scope - e.g. anonymous, user, merchant, reseller
        /// </summary>
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
        /// <summary>
        /// Version used for the request - e.g. 'v10'
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }
        /// <summary>
        /// Echo params send in the request
        /// </summary>
        [JsonPropertyName("params")]
        public Dictionary<string, string> Parameters { get; set; }
    }
}
