using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuickPay.SDK.Models.Shared
{
    public class Pong
    {
        /// <summary>
        /// Friendly message
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        public string Message { get; set; }
        /// <summary>
        /// API scope - e.g. anonymous, user, merchant, reseller
        /// </summary>
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }
        /// <summary>
        /// Version used for the request - e.g. 'v10'
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }
        /// <summary>
        /// Echo params send in the request
        /// </summary>
        [JsonProperty(PropertyName = "params")]
        public Dictionary<string, string> Parameters { get; set; }
    }
}
