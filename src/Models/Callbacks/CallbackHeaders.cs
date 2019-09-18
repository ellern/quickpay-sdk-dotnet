namespace QuickPay.SDK.Models.Callbacks
{
    public class CallbackHeaders
    {
        /// <summary>
        /// The type of resource that was created, changed or deleted
        /// </summary>
        public string ResourceType { get; set; }

        /// <summary>
        /// The account id of the resource owner - useful if receiving callbacks for several systems on the same url
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// API version of the callback-generating request
        /// </summary>
        public string ApiVersion { get; set; }

        /// <summary>
        /// Checksum of the entire raw callback request body - using HMAC with SHA256 as the cryptographic hash function. The checksum is signed using the Account's private key. We strongly recommend that you validate the checksum to ensure that the request is authentic.
        /// </summary>
        public string Checksum { get; set; }
    }
}
