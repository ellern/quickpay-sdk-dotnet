using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace QuickPay.SDK.Models.Shared
{
    public class Operation
    {
        /// <summary>
        /// Operation ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public int? Amount { get; set; }

        /// <summary>
        /// Type of operation
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// If the operation is pending
        /// </summary>
        public bool? Pending { get; set; }

        /// <summary>
        /// Acquirer specific data
        /// </summary>
        [JsonPropertyName("data")]
        public Dictionary<string, string> Data { get; set; }

        /// <summary>
        /// QuickPay status code
        /// </summary>
        [JsonPropertyName("qp_status_code")]
        public string QuickPayStatusCode { get; set; }

        /// <summary>
        /// QuickPay status message
        /// </summary>
        [JsonPropertyName("qp_status_msg")]
        public string QuickPayStatusMessage { get; set; }

        /// <summary>
        /// Acquirer status code
        /// </summary>
        [JsonPropertyName("aq_status_code")]
        public string AcquirerStatusCode { get; set; }

        /// <summary>
        /// Acquirer status message
        /// </summary>
        [JsonPropertyName("aq_status_msg")]
        public string AcquirerStatusMessage { get; set; }

        /// <summary>
        /// Operation callback url
        /// </summary>
        [JsonPropertyName("callback_url")]
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Did the callback succeed
        /// </summary>
        [JsonPropertyName("callback_success")]
        public bool? CallbackSuccess { get; set; }

        /// <summary>
        /// The http response code from the callback operation
        /// </summary>
        [JsonPropertyName("callback_response_code")]
        public string CallbackResponseCode { get; set; }

        /// <summary>
        /// Callback duration(ms)
        /// </summary>
        [JsonPropertyName("callback_duration")]
        public int? CallbackDuration { get; set; }

        /// <summary>
        /// Acquirer that processed this operation
        /// </summary>
        public string Acquirer { get; set; }

        /// <summary>
        /// 3D Secure status
        /// </summary>
        [JsonPropertyName("3d_secure_status")]
        public string SecureStatus { get; set; }

        /// <summary>
        /// Timestamp of callback
        /// </summary>
        [JsonPropertyName("callback_at")]
        public DateTime? Callback { get; set; }

        /// <summary>
        /// Timestamp of creation
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime Created { get; set; }

    }
}
